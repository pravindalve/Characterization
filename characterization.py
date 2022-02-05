import math
from math import log
import pandas as pd
import numpy as np
from scipy.optimize import curve_fit
from math import exp, log
from functions import *
pd.set_option("display.max_rows", None, "display.max_columns", None)


def characterization(SG, x_data, y_data):
    '''
    Function for characterizing TBP data which creates pseudo cuts and returns Ti, Tf, W%, V%, M%, MW, SG and Tb of pseudo cuts
    :param SG: Specific gravity of whole crude
    :param x_data: list of Weight percent data points
    :param y_data: list of Temperature data points in deg C
    :return: lists of Ti(K), Tf(K), W%, V%, M%, MW, SG and Tb(K)
    '''
    for i in range(len(y_data)):
        y_data[i] = (y_data[i] + 273.15)

    print(y_data)
    P = np.linspace(0, y_data[0], 100)

    rms_error = math.inf
    Pf = 0
    fitA = 0
    fitB = 0
    E = []

    for i in range(len(P)):
        P0 = P[i]

        def fun(x,A, B):
            '''function of versatile correlation using P0'''
            y = np.empty(len(x))
            for i in range(len(x)):
                y[i] = P0 * (((A/B)*log(1/(1 - x[i]/100)))**B + 1)

            return y

        parameters, covariance = curve_fit(fun, x_data, y_data)
        A = parameters[0]
        B = parameters[1]
        error = np.empty(len(x_data))
        for i in range(len(x_data)):
            error[i] = abs(y_data[i] - fun([x_data[i]], A, B))[0]
        E.append(np.sqrt(np.mean(error**2)))
        if rms_error > np.sqrt(np.mean(error**2)):
            Pf = P0
            rms_error = np.sqrt(np.mean(error ** 2))
            fitA = A
            fitB = B

    def fun1(y, A, B):
        '''inverse of versatile correlation using Pf'''
        x = np.empty(len(y))
        for i in range(len(y)):
            x[i] = 1 - exp(-B/A * ((y[i] - Pf)/Pf)**(1/B))
        return x

    def fun2(x, A, B):
        '''function of versatile correlation using Pf'''
        y = np.empty(len(x))
        for i in range(len(x)):
            y[i] = Pf * (((A / B) * log(1 / (1 - x[i] / 100))) ** B + 1)
        return y

    IBP = fun2([0], fitA, fitB)[0]
    FBP = fun2([99.99], fitA, fitB)[0]

    MeABP = (IBP + FBP)/2

    data = create_cuts(IBP, FBP)

    data["cut T"] = data["cuts_f"] - 273.15
    data["Tb"] = (data["cuts_i"] + data["cuts_f"])/2
    MeABP1 = FBP
    Kw = MeABP ** (1 / 3) / SG
    data["SG"] = data["Tb"] ** (1 / 3) / Kw

    while abs(MeABP - MeABP1) > 0.01:

        MeABP = MeABP1
        data["wt%"] = fun1(data["Tb"], fitA, fitB)
        # Checking material balance consistency
        data["cut wt%"] = data["wt%"].diff().fillna(data["wt%"])

        SG_t = 1 / sum(data["cut wt%"] / data["SG"])

        # Normalizing denisty

        data["SG"] = data["SG"] * SG / SG_t

        data["MW"] = calc_MW(data["Tb"], data["SG"])

        V = np.empty(len(data.index))

        V[0] = data.at[0,"wt%"]/data.at[0, "SG"]

        V[1:] = [(data.at[i,"wt%"]-data.at[i-1, "wt%"])/data.at[i, "SG"] for i in range(1,len(data.index))]

        V = data["wt%"].diff().fillna(data["wt%"])/data["SG"]

        V = V/sum(V)
        data["V%"] = V.cumsum()

        M = data["wt%"].diff().fillna(data["wt%"])/data["MW"]

        M = M/sum(M)
        data["M%"] = M.cumsum()

        MABP = sum(data["M%"].diff().fillna(data["M%"]) * data["Tb"])
        CABP = (sum(data["V%"].diff().fillna(data["V%"]) * data["Tb"]**(1/3)))**3

        MeABP1 = (MABP + CABP)/2
        Kw = MeABP1 ** (1 / 3) / SG
        data["SG"] = (data["Tb"])**(1/3) / Kw

    Ti = list(data["cuts_i"])
    Tf = list(data["cuts_f"])
    Wf = list(data["wt%"])
    Vf = list(data["V%"])
    Mf = list(data["M%"])
    MW = list(data["MW"])
    SG = list(data["SG"])
    Tb = list(data["Tb"])

    return [Ti, Tf, Wf, Vf, Mf, MW, SG, Tb]

# uncomment the code to see use of function
# SG = 0.809
#
# x = [2.9, 7.68859754431644, 15.1538050381994, 31.5823352252608, 42.4062967988428, 52.8464767354692, 62.7377406502837, 71.2394217843979, 74.0267929538375, 83.4788183586731, 88.4543077863666, 92.486636460126]
# y = [15, 65, 100, 150, 200, 250, 300, 350, 370, 450, 500, 550]
#
# ti, tf, wf, vf, mf, mw, sg, tb = characterization(SG, x, y)
# print(ti)
# print(tf)
# print(wf)
# print(vf)
# print(mf)
# print(mw)
# print(sg)
# print(tb)
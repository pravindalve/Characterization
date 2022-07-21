import math
from math import log
import pandas as pd
import numpy as np
from scipy.optimize import curve_fit
from math import exp, log, ceil, floor
pd.set_option("display.max_rows", None, "display.max_columns", None)

def create_cuts(IBP, FBP):
    cuts_i = []
    IBP = IBP - 273.15
    FBP = FBP - 273.15
    s = ceil(IBP / 10)
    s = s * 10
    if FBP > 460:
        cuts_i += list(np.linspace(s, 460, int((460 - s) / 10) + 1))
    else:
        cuts_i += list(np.linspace(s, floor(FBP / 10) * 10, int((floor(FBP / 10) * 10 - s) / 10) + 1))

    if FBP > 600:
        cuts_i += list(np.linspace(480, 600, int((600 - 480) / 20)+1))
    elif FBP > 460:
        cuts_i += list(np.linspace(480, floor(FBP / 20) * 20, int((floor(FBP / 20) * 20 - 460) / 20)))

    if FBP > 600:
        cuts_i += list(np.linspace(625, 600 + floor((FBP - 600) / 25) * 25, int((floor(FBP / 25) * 25 - 600) / 25 )))

    cuts_f = []
    for i in range(1, len(cuts_i)):
        cuts_f.append(cuts_i[i])

    del cuts_i[-1]

    columns = ["cuts_i", "cuts_f", "Tb", "SG", "wt%", "V%", "M%", "MW", "Tc", "Pc", "Tbr", "AF"]

    df = pd.DataFrame(index=range(len(cuts_i)), columns=columns)
    df["cuts_i"] = cuts_i
    df["cuts_f"] = cuts_f

    df["cuts_i"] += 273.15
    df["cuts_f"] += 273.15

    return df

def calc_MW(Tb, SG):
    # implementation of riazi daubert correlation
    MW = np.empty(len(Tb))
    for i in range(len(Tb)):
        MW[i] = 42.965*(exp(2.097*1e-4*(Tb[i]) - 7.78712*SG[i] + 2.08476*1e-3*(Tb[i])*SG[i]))*(Tb[i])**1.26007 * SG[i]**4.98308

    return MW

def calc_Tc(SG, Tb):
    # This function implements Lee-Kesler method for calculating Tc
    Tc = np.empty(len(SG))
    for i in range(len(SG)):
        Tc[i] = 189.8 + 450.6* SG[i] + (0.4244 + 0.1174*SG[i])*Tb[i] + (0.1441 - 1.0069 * SG[i]) * 1e5 / Tb[i]

    return Tc

def calc_Pc(SG, Tb):
    # This function implements Lee-Kesler method for calculating Pc
    Pc = np.empty(len(SG))
    for i in range(len(SG)):
        Pc[i] = exp(5.689 - 0.0566/SG[i] - (0.43639 + 4.1216 /SG[i] + 0.21343/SG[i]**2)* 1e-3*Tb[i] \
                + (0.47579 + 1.182/SG[i] + 0.15302/SG[i]**2)*1e-6*Tb[i]**2\
                - (2.4505 + 9.9099/SG[i]**2) * 1e-10 * Tb[i]**3)

    return Pc

def calc_AF(Pc, Tc, Tb, Kw):
    AF = np.empty(len(Pc))
    for i in range(len(Pc)):
        Tbr = Tb[i]/Tc[i]
        if Tbr <= 0.8:
            AF[i] = (-log(Pc[i]/1.01325) - 5.92714 + 6.09648/Tbr + 1.28862*log(Tbr) - 0.169347 * Tbr**6)/\
                    (15.2518 - 15.6875/Tbr - 13.4721 * log(Tbr) + 0.43577 * Tbr**6)

        elif Tbr > 0.8:
            AF[i] = -7.904 + 0.1352* Kw - 0.007465*Kw**2 + 8.359*Tbr + (1.408 - 0.01063 * Kw)/Tbr

    return AF

def characterization_wt(SG, x_data, y_data):
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
    P = np.linspace(90, y_data[0], 100)

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
        # Checking material balance consistency
        data["cut wt%"] = data["wt%"].diff().fillna(data["wt%"])
        SG_t = sum(data["cut wt%"]) / sum(data["cut wt%"] / data["SG"])

        # Normalizing denisty
        data["SG"] = data["SG"] * SG / SG_t

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

# x = [2.9, 7.68859754431644, 15.1538050381994, 31.5823352252608, 42.4062967988428, 52.8464767354692, 62.7377406502837, 71.2394217843979, 74.0267929538375, 83.4788183586731, 88.4543077863666, 92.486636460126]
# y = [15, 65, 100, 150, 200, 250, 300, 350, 370, 450, 500, 550]

# ti, tf, wf, vf, mf, mw, sg, tb = characterization_wt(SG, x, y)
# print(ti)
# print(tf)
# print(wf)
# print(vf)
# print(mf)
# print(mw)
# print(sg)
# print(tb)

def characterization_vol(SG, x_data, y_data):
    '''
        Function for characterizing TBP data which creates pseudo cuts and returns Ti, Tf, W%, V%, M%, MW, SG and Tb of pseudo cuts
        :param SG: Specific gravity of whole crude
        :param x_data: list of volume percent data points
        :param y_data: list of Temperature data points in deg C
        :return: lists of Ti(K), Tf(K), W%, V%, M%, MW, SG and Tb(K)
    '''

    for i in range(len(y_data)):
        y_data[i] = (y_data[i] + 273.15)

    P = np.linspace(90, y_data[0], 100)

    rms_error = math.inf
    Pf = 0
    fitA = 0
    fitB = 0
    E = []

    for i in range(len(P)):
        P0 = P[i]

        def fun(x,A, B):
            y = np.empty(len(x))
            for i in range(len(x)):
                y[i] =  P0 *(((A/B)*log(1/(1 - x[i]/100)))**B + 1)
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
        x = np.empty(len(y))
        for i in range(len(y)):
            x[i] = 1 - exp(-B/A * ((y[i] - Pf)/Pf)**(1/B))
        return x

    def fun2(x, A, B):
        y = np.empty(len(x))
        for i in range(len(x)):
            y[i] = Pf * (((A / B) * log(1 / (1 - x[i] / 100))) ** B + 1)
        return y

    fit_y = fun2(x_data, fitA, fitB)
    error = np.empty(len(x_data))
    for i in range(len(x_data)):
        error[i] = abs(y_data[i] - fun2([x_data[i]], fitA, fitB))[0]

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
        data["V%"] = fun1(data["Tb"], fitA, fitB)
        # Checking material balance consistency
        data["cut V%"] = data["V%"].diff().fillna(data["V%"])

        data["MW"] = calc_MW(data["Tb"], data["SG"])

        wt = np.empty(len(data.index))

        wt[0] = data.at[0,"V%"]*data.at[0, "SG"]

        wt[1:] = [(data.at[i,"V%"]-data.at[i-1, "V%"]) * data.at[i, "SG"] for i in range(1,len(data.index))]

        wt = data["V%"].diff().fillna(data["V%"]) * data["SG"]

        wt = wt/sum(wt)
        data["wt%"] = wt.cumsum()

        M = data["wt%"].diff().fillna(data["wt%"])/data["MW"]

        M = M/sum(M)
        data["M%"] = M.cumsum()

        MABP = sum(data["M%"].diff().fillna(data["M%"]) * data["Tb"])
        CABP = (sum(data["V%"].diff().fillna(data["V%"]) * data["Tb"]**(1/3)))**3
        MeABP1 = (MABP + CABP)/2
        Kw = MeABP1 ** (1 / 3) / SG
        data["SG"] = (data["Tb"])**(1/3) / Kw
        SG_t = sum(data["cut V%"] * data["SG"]) / sum(data["cut V%"])

        # Normalizing denisty
        data["SG"] = data["SG"] * SG / SG_t

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
# ti, tf, wf, vf, mf, mw, sg, tb = characterization_vol(SG, x, y)
# print(ti)
# print(tf)
# print(wf)
# print(vf)
# print(mf)
# print(mw)
# print(sg)
# print(tb)


def characterization_wt_SG(SG, x_data, y_data, SG_data):
    '''
        Function for characterizing TBP data which creates pseudo cuts and returns Ti, Tf, W%, V%, M%, MW, SG and Tb of pseudo cuts
        :param SG: Specific gravity of whole crude
        :param x_data: list of weight percent data points
        :param y_data: list of Temperature data points in deg C
        :param SG_data: list of Specific gravity data points
        :return: lists of Ti(K), Tf(K), W%, V%, M%, MW, SG and Tb(K)
    '''

    for i in range(len(y_data)):
        y_data[i] = (y_data[i] + 273.15)

    P = np.linspace(0, y_data[0], 100)

    rms_error = math.inf
    Pf = 0
    fitA = 0
    fitB = 0
    E = []

    for i in range(len(P)):
        P0 = P[i]


        def fun(x, A, B):
            y = np.empty(len(x))
            for i in range(len(x)):
                y[i] = P0 * (((A / B) * log(1 / (1 - x[i] / 100))) ** B + 1)
            return y


        parameters, covariance = curve_fit(fun, x_data, y_data)
        A = parameters[0]
        B = parameters[1]
        error = np.empty(len(x_data))
        for i in range(len(x_data)):
            error[i] = abs(y_data[i] - fun([x_data[i]], A, B))[0]
        E.append(np.sqrt(np.mean(error ** 2)))
        if rms_error > np.sqrt(np.mean(error ** 2)):
            Pf = P0
            rms_error = np.sqrt(np.mean(error ** 2))
            fitA = A
            fitB = B


    def fun1(y, A, B):
        x = np.empty(len(y))
        for i in range(len(y)):
            x[i] = 1 - exp(-B / A * ((y[i] - Pf) / Pf) ** (1 / B))
        return x


    def fun2(x, A, B):
        y = np.empty(len(x))
        for i in range(len(x)):
            y[i] = Pf * (((A / B) * log(1 / (1 - x[i] / 100))) ** B + 1)
        return y


    rms_error = math.inf
    Pf_SG = 0
    fitA_SG = 0
    fitB_SG = 0
    E_SG = []


    P_SG = np.linspace(0, 500, 1000)
    for i in range(len(P_SG)):
        P0 = P_SG[i]


        def fun_SG(x, A, B):
            y = np.empty(len(x))
            for i in range(len(x)):
                y[i] = P0 * (((A / B) * log(1 / (1 - x[i]))) ** B + 1)
            return y


        try:
            parameters, covariance = curve_fit(fun_SG, SG_data, y_data)
        except RuntimeError:
            parameters = [1, 1]
            print(P0)

        A_SG = parameters[0]
        B_SG = parameters[1]
        error_SG = np.empty(len(SG_data))
        for i in range(len(SG_data)):
            error_SG[i] = abs(y_data[i] - fun_SG([SG_data[i]], A_SG, B_SG))[0]
        E_SG.append(np.sqrt(np.mean(error_SG ** 2)))
        if rms_error > np.sqrt(np.mean(error_SG ** 2)):
            Pf_SG = P0
            rms_error = np.sqrt(np.mean(error_SG ** 2))
            fitA_SG = A_SG
            fitB_SG = B_SG


    def fun1_SG(y, A, B):
        x = np.empty(len(y))
        for i in range(len(y)):
            x[i] = 1 - exp(-B / A * ((y[i] - Pf_SG) / Pf_SG) ** (1 / B))
        return x


    def fun2_SG(x, A, B):
        y = np.empty(len(x))
        for i in range(len(x)):
            y[i] = Pf_SG * (((A / B) * log(1 / (1 - x[i]))) ** B + 1)
        return y


    IBP = fun2([0], fitA, fitB)[0]
    FBP = fun2([99.99], fitA, fitB)[0]

    data = create_cuts(IBP, FBP)

    data["cut T"] = data["cuts_f"] - 273.15
    data["Tb"] = (data["cuts_i"] + data["cuts_f"]) / 2

    # calculation of SG and wt%
    data["wt%"] = fun1(data["Tb"], fitA, fitB)
    data["cut wt%"] = data["wt%"].diff().fillna(data["wt%"])
    data["SG"] = fun1_SG(data["Tb"], fitA_SG, fitB_SG)

    # Normalising SG
    SG_t = 1 / sum(data["cut wt%"] / data["SG"])
    data["SG"] = data["SG"] * SG / SG_t

    data["MW"] = calc_MW(data["Tb"], data["SG"])

    V = np.empty(len(data.index))

    V[0] = data.at[0, "wt%"] / data.at[0, "SG"]

    V[1:] = [(data.at[i, "wt%"] - data.at[i - 1, "wt%"]) / data.at[i, "SG"] for i in range(1, len(data.index))]

    V = data["wt%"].diff().fillna(data["wt%"]) / data["SG"]

    V = V / sum(V)
    data["V%"] = V.cumsum()

    M = data["wt%"].diff().fillna(data["wt%"]) / data["MW"]

    M = M / sum(M)
    data["M%"] = M.cumsum()

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
# x = [ 7.68859754431644, 15.1538050381994, 31.5823352252608, 42.4062967988428, 52.8464767354692, 62.7377406502837, 71.2394217843979, 74.0267929538375, 83.4788183586731, 88.4543077863666, 92.486636460126]
# y = [ 65, 100, 150, 200, 250, 300, 350, 370, 450, 500, 550]
# SG_d = [0.647455170862764, 0.706398861451607, 0.749777780304995, 0.788281101028304, 0.819733528157335,
#            0.84372915687493, 0.865974897719598, 0.882076096202821, 0.898995318013918, 0.913907839238595,
#            0.924511278748632]
# ti, tf, wf, vf, mf, mw, sg, tb = characterization_wt_SG(SG, x, y, SG_d)
# print(ti)
# print(tf)
# print(wf)
# print(vf)
# print(mf)
# print(mw)
# print(sg)
# print(tb)

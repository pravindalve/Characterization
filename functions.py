import pandas as pd
from math import ceil, floor, exp, log
import numpy as np

def create_cuts(IBP, FBP):
    cuts_i = []
    IBP = IBP - 273.15
    FBP = FBP - 273.15
    s = ceil(IBP / 10)
    s = s * 10
    if FBP > 460:
        cuts_i += list(np.linspace(s, 460, int((460 - s) / 10) + 1))
    else:
        cuts_i += np.linspace(s, floor(FBP / 10) * 10, int((floor(FBP / 10) * 10 - s) / 10) + 1)

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
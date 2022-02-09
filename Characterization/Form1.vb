Imports System.IO
Imports System.Reflection
Imports System.Windows.Forms
Imports DWSIM.ExtensionMethods
Imports DWSIM.Interfaces
Imports DWSIM.Thermodynamics.BaseClasses
Imports DWSIM.Thermodynamics.PetroleumCharacterization.Methods
Imports DWSIM.Thermodynamics.PropertyPackages.Auxiliary
Imports Python.Included
Imports Python.Runtime

Public Class Form1

    Dim ccol As Dictionary(Of String, Compound)

    Public frm As IFlowsheet
    Dim TableChanged As Boolean = False
    Dim Populated As Boolean = False
    Dim id As Integer

    Private Sub DataGridView2_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellValueChanged

        If Populated Then

            TableChanged = True

        End If

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ChangeDefaultFont()

        AddHandler AppDomain.CurrentDomain.AssemblyResolve, New ResolveEventHandler(AddressOf LoadFromPluginsFolder)

        Me.ComboBoxAF.SelectedIndex = 0
        Me.ComboBoxMW.SelectedIndex = 0
        Me.ComboBoxSG.SelectedIndex = 0
        Me.ComboBoxTC.SelectedIndex = 0
        Me.ComboBoxPC.SelectedIndex = 0

        Dim rd As New Random

        id = rd.Next(1000, 9999)

        Me.TextBox1.Text = "OIL_" & id

        Dim su = frm.FlowsheetOptions.SelectedUnitSystem

        With Me.DataGridView2.Columns
            .Item(2).HeaderText += " (" & su.temperature & ")"
            .Item(4).HeaderText += " (" & su.molecularWeight & ")"
            .Item(5).HeaderText += " (" & su.temperature & ")"
            .Item(6).HeaderText += " (" & su.pressure & ")"
            .Item(8).HeaderText += " (" & su.cinematic_viscosity & ")"
            .Item(9).HeaderText += " (" & su.cinematic_viscosity & ")"
        End With

        InitializePython()

    End Sub

    Private Shared Function LoadFromPluginsFolder(ByVal sender As Object, ByVal args As ResolveEventArgs) As Assembly

        Dim assemblyPath1 As String = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly.Location), New AssemblyName(args.Name).Name + ".dll")
        Dim assemblyPath2 As String = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly.Location), "plugins", New AssemblyName(args.Name).Name + ".dll")

        If Not File.Exists(assemblyPath1) Then
            If Not File.Exists(assemblyPath2) Then
                Return Nothing
            Else
                Dim assembly As Assembly = Assembly.LoadFrom(assemblyPath2)
                Return assembly
            End If
        Else
            Dim assembly As Assembly = Assembly.LoadFrom(assemblyPath1)
            Return assembly
        End If

    End Function

    Private Sub InitializePython()

        Dim fp As New FormSetupPython()

        Me.Enabled = False

        fp.Show(Me)

        Task.Run(Sub()

                     fp.UIThread(Sub() fp.Label1.Text = "Extracting Python 3.7...")
                     fp.UIThread(Sub() fp.ProgressBar1.Value = 10)

                     Installer.SetupPython().Wait()

                     DWSIM.GlobalSettings.Settings.ShutdownPythonEnvironment()

                     fp.UIThread(Sub() fp.Label1.Text = "Installing numpy...")
                     fp.UIThread(Sub() fp.ProgressBar1.Value = 20)

                     Installer.InstallWheel(Reflection.Assembly.GetExecutingAssembly(), "numpy-1.20.0-cp37-cp37m-win_amd64.whl").Wait()

                     fp.UIThread(Sub() fp.Label1.Text = "Installing scipy...")
                     fp.UIThread(Sub() fp.ProgressBar1.Value = 40)

                     Installer.InstallWheel(Reflection.Assembly.GetExecutingAssembly(), "scipy-1.7.0-cp37-cp37m-win_amd64.whl").Wait()

                     fp.UIThread(Sub() fp.Label1.Text = "Installing pandas...")
                     fp.UIThread(Sub() fp.ProgressBar1.Value = 60)

                     Installer.InstallWheel(Reflection.Assembly.GetExecutingAssembly(), "pandas-1.3.0-cp37-cp37m-win_amd64.whl").Wait()

                     fp.UIThread(Sub() fp.Label1.Text = "Installing pytz...")
                     fp.UIThread(Sub() fp.ProgressBar1.Value = 80)

                     Installer.InstallWheel(Reflection.Assembly.GetExecutingAssembly(), "pytz-2021.3-py2.py3-none-any.whl").Wait()

                     fp.UIThread(Sub() fp.Label1.Text = "Installing dateutil...")
                     fp.UIThread(Sub() fp.ProgressBar1.Value = 85)

                     Installer.InstallWheel(Reflection.Assembly.GetExecutingAssembly(), "python_dateutil-2.8.2-py2.py3-none-any.whl").Wait()

                     fp.UIThread(Sub() fp.Label1.Text = "Installing six...")
                     fp.UIThread(Sub() fp.ProgressBar1.Value = 90)

                     Installer.InstallWheel(Reflection.Assembly.GetExecutingAssembly(), "six-1.16.0-py2.py3-none-any.whl").Wait()

                     fp.UIThread(Sub() fp.Label1.Text = "Initializing the Python Environment...")
                     fp.UIThread(Sub() fp.ProgressBar1.Value = 95)

                     DWSIM.GlobalSettings.Settings.InitializePythonEnvironment(Path.Combine(Installer.InstallPath, Installer.InstallDirectory))

                     fp.UIThread(Sub() fp.Label1.Text = "Finished!")
                     fp.UIThread(Sub() fp.ProgressBar1.Value = 100)

                 End Sub).ContinueWith(Sub(t)

                                           UIThread(Sub() fp.Close())

                                           If t.Exception IsNot Nothing Then

                                               MessageBox.Show(t.Exception.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                                           Else

                                               UIThread(Sub() Enabled = True)

                                           End If

                                       End Sub)


    End Sub

    Private Sub DataGridViewTBP_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles DataGridViewTBP.KeyDown
        If e.KeyCode = Keys.Delete And e.Modifiers = Keys.Shift Then
            Dim toremove As New ArrayList
            For Each c As DataGridViewCell In CType(sender, DataGridView).SelectedCells
                If Not toremove.Contains(c.RowIndex) Then toremove.Add(c.RowIndex)
            Next
            Try
                For Each i As Integer In toremove
                    CType(sender, DataGridView).Rows.RemoveAt(i)
                Next
            Catch ex As Exception

            End Try
        ElseIf e.KeyCode = Keys.V And e.Modifiers = Keys.Control Then
            PasteData(sender)
        ElseIf e.KeyCode = Keys.Delete Then
            For Each c As DataGridViewCell In CType(sender, DataGridView).SelectedCells
                c.Value = ""
            Next
        End If
    End Sub

    Public Sub PasteData(ByRef dgv As DataGridView)
        Dim tArr() As String
        Dim arT() As String
        Dim i, ii As Integer
        Dim c, cc, r As Integer

        tArr = Clipboard.GetText().Split(Environment.NewLine)

        If dgv.SelectedCells.Count > 0 Then
            r = dgv.SelectedCells(0).RowIndex
            c = dgv.SelectedCells(0).ColumnIndex
        Else
            r = 0
            c = 0
        End If
        For i = 0 To tArr.Length - 1
            If tArr(i) <> "" Then
                arT = tArr(i).Split(vbTab)
                For ii = 0 To arT.Length - 1
                    If r > dgv.Rows.Count - 1 Then
                        dgv.Rows.Add()
                        dgv.Rows(0).Cells(0).Selected = True
                    End If
                Next
                r = r + 1
            End If
        Next
        If dgv.SelectedCells.Count > 0 Then
            r = dgv.SelectedCells(0).RowIndex
            c = dgv.SelectedCells(0).ColumnIndex
        Else
            r = 0
            c = 0
        End If
        For i = 0 To tArr.Length - 1
            If tArr(i) <> "" Then
                arT = tArr(i).Split(vbTab)
                cc = c
                For ii = 0 To arT.Length - 1
                    cc = GetNextVisibleCol(dgv, cc)
                    If cc > dgv.ColumnCount - 1 Then Exit For
                    dgv.Item(cc, r).Value = arT(ii).TrimStart
                    cc = cc + 1
                Next
                r = r + 1
            End If
        Next

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim charact As String

        Using filestr As IO.Stream = Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Characterization.characterization.py")
            Using r As New StreamReader(filestr)
                charact = r.ReadToEnd()
            End Using
        End Using

        Using Py.GIL()

            Using scope = Py.CreateScope()

                scope.Exec(charact)

                Dim np As Object = Py.Import("numpy")
                Dim charac1 As Object = scope.Get("characterization")

                Dim SG = TextBoxSG.Text.ToDoubleFromCurrent()

                Dim datax, datay As New List(Of Double)

                For Each row As DataGridViewRow In DataGridViewTBP.Rows
                    If row.Cells(0).Value IsNot Nothing And row.Cells(0).Value IsNot Nothing Then
                        Try
                            datax.Add(row.Cells(0).Value)
                            datay.Add(row.Cells(1).Value)
                        Catch ex As Exception
                        End Try
                    End If
                Next

                Dim x = np.array(datax)
                Dim y = np.array(datay)

                Dim result As Object = charac1(SG, x, y)

                'return [Ti, Tf, Wf, Vf, Mf, MW, SG, Tb]

                Dim TiP As Object = result(0)
                Dim TfP As Object = result(1)
                Dim WfP As Object = result(2)
                Dim VfP As Object = result(3)
                Dim MfP As Object = result(4)
                Dim MWP As Object = result(5)
                Dim SGP As Object = result(6)
                Dim TbP As Object = result(7)

                Dim Ti, Tf, Wf, Vf, Mf, MW, SG2, Tb As New List(Of Double)

                For i As Integer = 0 To DirectCast(TiP, PyObject).Length - 1
                    Ti.Add(Double.Parse(TiP.Item(i).ToString(), Globalization.CultureInfo.InvariantCulture))
                    Tf.Add(Double.Parse(TfP.Item(i).ToString(), Globalization.CultureInfo.InvariantCulture))
                    Wf.Add(Double.Parse(WfP.Item(i).ToString(), Globalization.CultureInfo.InvariantCulture))
                    Vf.Add(Double.Parse(VfP.Item(i).ToString(), Globalization.CultureInfo.InvariantCulture))
                    Mf.Add(Double.Parse(MfP.Item(i).ToString(), Globalization.CultureInfo.InvariantCulture))
                    MW.Add(Double.Parse(MWP.Item(i).ToString(), Globalization.CultureInfo.InvariantCulture))
                    SG2.Add(Double.Parse(SGP.Item(i).ToString(), Globalization.CultureInfo.InvariantCulture))
                    Tb.Add(Double.Parse(TbP.Item(i).ToString(), Globalization.CultureInfo.InvariantCulture))
                Next

                Characterize(Mf, MW, SG2, Tb.Select(Function(d) DWSIM.SharedClasses.SystemsOfUnits.Converter.ConvertToSI("C", d)).ToList())

                Me.Focus()
                Me.BringToFront()

            End Using

        End Using


    End Sub

    Public Sub Characterize(dMF As List(Of Double), dMW As List(Of Double), dSG As List(Of Double), dTB As List(Of Double))

        Dim T1 = 37.8 + 273.15
        Dim T2 = 98.9 + 273.15

        Dim dV1, dV2, dVA, dVB As New List(Of Double)

        Dim n = dMF.Count - 1

        Dim i As Integer

        For i = 0 To n
            dV1.Add(PropertyMethods.Visc37_Abbott(dTB(i), dSG(i)))
            dV2.Add(PropertyMethods.Visc98_Abbott(dTB(i), dSG(i)))
        Next

        For i = 0 To n
            dVA.Add(PropertyMethods.ViscWaltherASTM_A(T1, dV1(i), T2, dV2(i)))
            dVB.Add(PropertyMethods.ViscWaltherASTM_B(T1, dV1(i), T2, dV2(i)))
        Next

        Dim prop As New PROPS
        Dim prop2 As New DWSIM.Thermodynamics.Utilities.PetroleumCharacterization.Methods.GL

        ccol = New Dictionary(Of String, Compound)
        ccol.Clear()

        For i = 0 To n

            Dim cprop As New ConstantProperties()

            With cprop

                .NBP = dTB(i)
                .Normal_Boiling_Point = .NBP
                .OriginalDB = "DWSIM"

                'SG
                .PF_SG = dSG(i)

                'VISC
                .PF_Tv1 = T1
                .PF_Tv2 = T2
                .PF_v1 = dV1(i)
                .PF_v2 = dV2(i)
                .PF_vA = dVA(i)
                .PF_vB = dVB(i)

                'MW
                .PF_MM = dMW(i)
                .Molar_Weight = dMW(i)

                'Tc
                Select Case Me.ComboBoxTC.SelectedItem.ToString
                    Case "Riazi-Daubert (1985)"
                        .Critical_Temperature = PropertyMethods.Tc_RiaziDaubert(.NBP, .PF_SG)
                    Case "Riazi (2005)"
                        .Critical_Temperature = PropertyMethods.Tc_Riazi(.NBP, .PF_SG)
                    Case "Lee-Kesler (1976)"
                        .Critical_Temperature = PropertyMethods.Tc_LeeKesler(.NBP, .PF_SG)
                    Case "Farah (2006)"
                        .Critical_Temperature = PropertyMethods.Tc_Farah(.PF_vA, .PF_vB, .NBP, .PF_SG)
                End Select

                'Pc
                Select Case Me.ComboBoxPC.SelectedItem.ToString
                    Case "Riazi-Daubert (1985)"
                        .Critical_Pressure = PropertyMethods.Pc_RiaziDaubert(.NBP, .PF_SG)
                    Case "Riazi (2005)"
                        .Critical_Pressure = PropertyMethods.Pc_Riazi(.NBP, .PF_SG)
                    Case "Lee-Kesler (1976)"
                        .Critical_Pressure = PropertyMethods.Pc_LeeKesler(.NBP, .PF_SG)
                    Case "Farah (2006)"
                        .Critical_Pressure = PropertyMethods.Pc_Farah(.PF_vA, .PF_vB, .NBP, .PF_SG)
                End Select

                'Af
                Select Case Me.ComboBoxAF.SelectedItem.ToString
                    Case "Lee-Kesler (1976)"
                        .Acentric_Factor = PropertyMethods.AcentricFactor_LeeKesler(.Critical_Temperature, .Critical_Pressure, .NBP)
                    Case "Korsten (2000)"
                        .Acentric_Factor = PropertyMethods.AcentricFactor_Korsten(.Critical_Temperature, .Critical_Pressure, .NBP)
                End Select

                .Normal_Boiling_Point = .NBP

                .Molar_Weight = dMW(i)
                .IsPF = 1

                If Not Double.IsNaN(.NBP.GetValueOrDefault) Then
                    .Name = "C_" & id & "_NBP_" & CInt(.NBP.GetValueOrDefault - 273.15).ToString
                    .CAS_Number = id.ToString() & "-" & CInt(.NBP.GetValueOrDefault()).ToString()
                Else
                    .Name = "C_" & id & "_NBP_" & i.ToString()
                    .CAS_Number = id.ToString() & "-" & i.ToString()
                End If

                .PF_Watson_K = (1.8 * .NBP.GetValueOrDefault) ^ (1 / 3) / .PF_SG.GetValueOrDefault
                .Critical_Compressibility = PROPS.Zc1(.Acentric_Factor)
                .Critical_Volume = 8314 * .Critical_Compressibility * .Critical_Temperature / .Critical_Pressure
                .Z_Rackett = PROPS.Zc1(.Acentric_Factor)
                If .Z_Rackett < 0 Then .Z_Rackett = 0.2

                Dim tmp = prop2.calculate_Hf_Sf(.PF_SG, .Molar_Weight, .NBP)

                .IG_Enthalpy_of_Formation_25C = tmp(0)
                .IG_Entropy_of_Formation_25C = tmp(1)
                .IG_Gibbs_Energy_of_Formation_25C = tmp(0) - 298.15 * tmp(1)

                .Formula = "C" & CDbl(tmp(2)).ToString("N2") & "H" & CDbl(tmp(3)).ToString("N2")

                Dim methods2 As New PROPS
                Dim methods As New DWSIM.Thermodynamics.Utilities.Hypos.Methods.HYP

                .HVap_A = methods.DHvb_Vetere(.Critical_Temperature, .Critical_Pressure, .Normal_Boiling_Point) / .Molar_Weight

                .Critical_Compressibility = PROPS.Zc1(.Acentric_Factor)
                .Critical_Volume = PROPS.Vc(.Critical_Temperature, .Critical_Pressure, .Acentric_Factor, .Critical_Compressibility)
                .Z_Rackett = PROPS.Zc1(.Acentric_Factor)
                If .Z_Rackett < 0 Then
                    .Z_Rackett = 0.2
                End If

                .Chao_Seader_Acentricity = .Acentric_Factor
                .Chao_Seader_Solubility_Parameter = ((.HVap_A * .Molar_Weight - 8.314 * .Normal_Boiling_Point) * 238.846 * PROPS.liq_dens_rackett(.Normal_Boiling_Point, .Critical_Temperature, .Critical_Pressure, .Acentric_Factor, .Molar_Weight) / .Molar_Weight / 1000000.0) ^ 0.5
                .Chao_Seader_Liquid_Molar_Volume = 1 / PROPS.liq_dens_rackett(.Normal_Boiling_Point, .Critical_Temperature, .Critical_Pressure, .Acentric_Factor, .Molar_Weight) * .Molar_Weight / 1000 * 1000000.0

                methods2 = Nothing
                methods = Nothing

                .ID = -id - i + 1

            End With

            Dim subst As New Compound(cprop.Name, "")

            With subst
                If i = 0 Then
                    .MoleFraction = dMF(i)
                Else
                    .MoleFraction = dMF(i) - dMF(i - 1)
                End If
                .ConstantProperties = cprop
                .Name = cprop.Name
                .PetroleumFraction = True
            End With

            ccol.Add(cprop.Name, subst)

        Next

        'Adjust Acentric Factors and Rackett parameters to fit NBP and Density

        Dim dfit As New DWSIM.Thermodynamics.Utilities.PetroleumCharacterization.Methods.DensityFitting
        Dim prvsfit As New DWSIM.Thermodynamics.Utilities.PetroleumCharacterization.Methods.PRVSFitting
        Dim srkvsfit As New DWSIM.Thermodynamics.Utilities.PetroleumCharacterization.Methods.SRKVSFitting
        Dim nbpfit As New DWSIM.Thermodynamics.Utilities.PetroleumCharacterization.Methods.NBPFitting With {.Flowsheet = frm}
        Dim tms As New DWSIM.Thermodynamics.Streams.MaterialStream("", "")
        Dim pp As DWSIM.Thermodynamics.PropertyPackages.PropertyPackage
        Dim fzra, fw, fprvs, fsrkvs As Double

        If frm.Options.PropertyPackages.Count > 0 Then
            pp = frm.Options.SelectedPropertyPackage
            If TypeOf pp Is DWSIM.Thermodynamics.PropertyPackages.PengRobinsonPropertyPackage Then
                DirectCast(pp, DWSIM.Thermodynamics.PropertyPackages.PengRobinsonPropertyPackage).m_pr.BIPChanged = True
            ElseIf TypeOf pp Is DWSIM.Thermodynamics.PropertyPackages.SRKPropertyPackage Then
                DirectCast(pp, DWSIM.Thermodynamics.PropertyPackages.SRKPropertyPackage).m_pr.BIPChanged = True
            End If
        Else
            pp = New DWSIM.Thermodynamics.PropertyPackages.PengRobinsonPropertyPackage()
        End If

        For Each c As Compound In ccol.Values
            tms.Phases(0).Compounds.Add(c.Name, c)
        Next

        Dim recalcVc As Boolean = False

        i = 0
        For Each c As Compound In ccol.Values
            If Me.CheckBoxADJAF.Checked Then
                If c.ConstantProperties.Acentric_Factor < 0 Then
                    c.ConstantProperties.Acentric_Factor = 0.5
                    recalcVc = True
                End If
                With nbpfit
                    ._pp = pp
                    ._ms = tms
                    ._idx = i
                    fw = .MinimizeError()
                End With
                With c.ConstantProperties
                    c.ConstantProperties.Acentric_Factor *= fw
                    c.ConstantProperties.Z_Rackett = PROPS.Zc1(c.ConstantProperties.Acentric_Factor)
                    If .Z_Rackett < 0 Then
                        .Z_Rackett = 0.2
                        recalcVc = True
                    End If
                    .Critical_Compressibility = PROPS.Zc1(.Acentric_Factor)
                    .Critical_Volume = PROPS.Vc(.Critical_Temperature, .Critical_Pressure, .Acentric_Factor, .Critical_Compressibility)
                End With
            End If
            If Me.CheckBoxADJZRA.Checked Then
                With dfit
                    ._comp = c
                    fzra = .MinimizeError()
                End With
                With c.ConstantProperties
                    .Z_Rackett *= fzra
                    If .Critical_Compressibility < 0 Or recalcVc Then
                        .Critical_Compressibility = .Z_Rackett
                        .Critical_Volume = PROPS.Vc(.Critical_Temperature, .Critical_Pressure, .Acentric_Factor, .Critical_Compressibility)
                    End If
                End With
            End If
            c.ConstantProperties.PR_Volume_Translation_Coefficient = 1
            prvsfit._comp = c
            fprvs = prvsfit.MinimizeError()
            With c.ConstantProperties
                If Math.Abs(fprvs) < 99.0# Then .PR_Volume_Translation_Coefficient *= fprvs Else .PR_Volume_Translation_Coefficient = 0.0#
            End With
            c.ConstantProperties.SRK_Volume_Translation_Coefficient = 1
            srkvsfit._comp = c
            fsrkvs = srkvsfit.MinimizeError()
            With c.ConstantProperties
                If Math.Abs(fsrkvs) < 99.0# Then .SRK_Volume_Translation_Coefficient *= fsrkvs Else .SRK_Volume_Translation_Coefficient = 0.0#
            End With
            recalcVc = False
            i += 1
        Next

        pp = Nothing
        dfit = Nothing
        nbpfit = Nothing
        tms = Nothing

        Dim nm, fm, nbp, sgi, mm, ct, cp, af, visc1, visc2, prvs, srkvs As String

        Populated = False

        Dim nf = frm.FlowsheetOptions.NumberFormat
        Dim su = frm.FlowsheetOptions.SelectedUnitSystem

        Me.DataGridView2.Rows.Clear()
        For Each subst As Compound In ccol.Values
            With subst
                nm = .Name
                fm = Format(.MoleFraction, nf)
                nbp = Format(DWSIM.SharedClasses.SystemsOfUnits.Converter.ConvertFromSI(su.temperature, .ConstantProperties.NBP), nf)
                sgi = Format(.ConstantProperties.PF_SG, nf)
                mm = Format(.ConstantProperties.PF_MM, nf)
                ct = Format(DWSIM.SharedClasses.SystemsOfUnits.Converter.ConvertFromSI(su.temperature, .ConstantProperties.Critical_Temperature), nf)
                cp = Format(DWSIM.SharedClasses.SystemsOfUnits.Converter.ConvertFromSI(su.pressure, .ConstantProperties.Critical_Pressure), nf)
                af = Format(.ConstantProperties.Acentric_Factor, nf)
                visc1 = Format(DWSIM.SharedClasses.SystemsOfUnits.Converter.ConvertFromSI(su.cinematic_viscosity, .ConstantProperties.PF_v1), "E")
                visc2 = Format(DWSIM.SharedClasses.SystemsOfUnits.Converter.ConvertFromSI(su.cinematic_viscosity, .ConstantProperties.PF_v2), "E")
                prvs = Format(.ConstantProperties.PR_Volume_Translation_Coefficient, "N6")
                srkvs = Format(.ConstantProperties.SRK_Volume_Translation_Coefficient, "N6")
            End With
            Me.DataGridView2.Rows.Add(New Object() {nm, fm, nbp, sgi, mm, ct, cp, af, visc1, visc2, prvs, srkvs})
        Next

        Populated = True

    End Sub

    Private Sub KButton3_Click(sender As Object, e As EventArgs) Handles KButton3.Click

        If TableChanged Then ReadFromTable()

        'finalize button

        Dim corr As String = Me.TextBox1.Text
        Dim tmpcomp As New DWSIM.Thermodynamics.BaseClasses.ConstantProperties
        Dim subst As DWSIM.Thermodynamics.BaseClasses.Compound
        Dim gObj As DWSIM.Drawing.SkiaSharp.GraphicObjects.GraphicObject = Nothing
        Dim idx As Integer = 0

        Dim ms As New DWSIM.Thermodynamics.Streams.MaterialStream("", "")
        ms.SetFlowsheet(frm)
        If frm.Options.PropertyPackages.Count > 0 Then
            ms.PropertyPackage = frm.Options.SelectedPropertyPackage
            If TypeOf ms.PropertyPackage Is DWSIM.Thermodynamics.PropertyPackages.PengRobinsonPropertyPackage Then
                DirectCast(ms.PropertyPackage, DWSIM.Thermodynamics.PropertyPackages.PengRobinsonPropertyPackage).m_pr.BIPChanged = True
            ElseIf TypeOf ms.PropertyPackage Is DWSIM.Thermodynamics.PropertyPackages.SRKPropertyPackage Then
                DirectCast(ms.PropertyPackage, DWSIM.Thermodynamics.PropertyPackages.SRKPropertyPackage).m_pr.BIPChanged = True
            End If
        Else
            ms.PropertyPackage = New DWSIM.Thermodynamics.PropertyPackages.PengRobinsonPropertyPackage()
        End If
        For Each subst In ccol.Values
            ms.Phases(0).Compounds.Add(subst.Name, subst)
            ms.Phases(1).Compounds.Add(subst.Name, New Compound(subst.Name, "") With {.ConstantProperties = subst.ConstantProperties})
            ms.Phases(2).Compounds.Add(subst.Name, New Compound(subst.Name, "") With {.ConstantProperties = subst.ConstantProperties})
            ms.Phases(3).Compounds.Add(subst.Name, New Compound(subst.Name, "") With {.ConstantProperties = subst.ConstantProperties})
            ms.Phases(4).Compounds.Add(subst.Name, New Compound(subst.Name, "") With {.ConstantProperties = subst.ConstantProperties})
            ms.Phases(5).Compounds.Add(subst.Name, New Compound(subst.Name, "") With {.ConstantProperties = subst.ConstantProperties})
            ms.Phases(6).Compounds.Add(subst.Name, New Compound(subst.Name, "") With {.ConstantProperties = subst.ConstantProperties})
            ms.Phases(7).Compounds.Add(subst.Name, New Compound(subst.Name, "") With {.ConstantProperties = subst.ConstantProperties})
        Next

        If Not frm.FrmStSim1.initialized Then frm.FrmStSim1.Init()

        For Each subst In ccol.Values
            tmpcomp = subst.ConstantProperties
            frm.Options.NotSelectedComponents.Add(tmpcomp.Name, tmpcomp)
            idx = frm.FrmStSim1.AddCompToGrid(tmpcomp)
            frm.FrmStSim1.AddCompToSimulation(tmpcomp.Name)
        Next

        Dim myMStr As New DWSIM.Drawing.SkiaSharp.GraphicObjects.Shapes.MaterialStreamGraphic(100, 100, 20, 20)
        myMStr.LineWidth = 2
        myMStr.Fill = True
        myMStr.Tag = corr
        gObj = myMStr
        gObj.Name = "MAT-" & Guid.NewGuid.ToString
        'OBJETO DWSIM
        Dim myCOMS As DWSIM.Thermodynamics.Streams.MaterialStream = New DWSIM.Thermodynamics.Streams.MaterialStream(myMStr.Name, frm.GetTranslatedString("CorrentedeMatria"))
        myCOMS.GraphicObject = myMStr
        myMStr.Owner = myCOMS
        frm.AddComponentsRows(myCOMS)
        If frm.Options.PropertyPackages.Count > 0 Then
            myCOMS.PropertyPackage = frm.Options.SelectedPropertyPackage
        Else
            myCOMS.PropertyPackage = New DWSIM.Thermodynamics.PropertyPackages.PengRobinsonPropertyPackage()
        End If
        myCOMS.ClearAllProps()
        Dim wtotal As Double = 0
        For Each subst In ccol.Values
            wtotal += subst.MoleFraction.GetValueOrDefault * subst.ConstantProperties.Molar_Weight
        Next
        For Each subst In ccol.Values
            subst.MassFraction = subst.MoleFraction.GetValueOrDefault * subst.ConstantProperties.Molar_Weight / wtotal
        Next
        For Each subst In ccol.Values
            With myCOMS.Phases(0).Compounds
                .Item(subst.Name).ConstantProperties = subst.ConstantProperties
                .Item(subst.Name).MassFraction = subst.MassFraction
                .Item(subst.Name).MoleFraction = subst.MoleFraction
            End With
            myCOMS.Phases(1).Compounds.Item(subst.Name).ConstantProperties = subst.ConstantProperties
            myCOMS.Phases(2).Compounds.Item(subst.Name).ConstantProperties = subst.ConstantProperties
        Next
        myCOMS.SetFlowsheet(frm)
        frm.AddSimulationObject(myCOMS)
        frm.AddGraphicObject(gObj)
        frm.FormSurface.Invalidate()

        If MessageBox.Show("Do you want to close this window?", "Close Window", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Me.Close()
        End If

    End Sub

    Private Sub ReadFromTable()

        Dim su = frm.FlowsheetOptions.SelectedUnitSystem
        For Each row As DataGridViewRow In DataGridView2.Rows
            Dim subst = ccol(row.Cells(0).Value)
            With subst
                .MoleFraction = row.Cells(1).Value.ToString().ToDoubleFromCurrent()
                .ConstantProperties.NBP = row.Cells(2).Value.ToString().ToDoubleFromCurrent().ConvertToSI(su.temperature)
                .ConstantProperties.Normal_Boiling_Point = row.Cells(2).Value.ToString().ToDoubleFromCurrent().ConvertToSI(su.temperature)
                .ConstantProperties.PF_SG = row.Cells(3).Value.ToString().ToDoubleFromCurrent()
                .ConstantProperties.PF_MM = row.Cells(4).Value.ToString().ToDoubleFromCurrent()
                .ConstantProperties.Molar_Weight = row.Cells(4).Value.ToString().ToDoubleFromCurrent()
                .ConstantProperties.Critical_Temperature = row.Cells(5).Value.ToString().ToDoubleFromCurrent().ConvertToSI(su.temperature)
                .ConstantProperties.Critical_Pressure = row.Cells(6).Value.ToString().ToDoubleFromCurrent().ConvertToSI(su.pressure)
                .ConstantProperties.Acentric_Factor = row.Cells(7).Value.ToString().ToDoubleFromCurrent()
                .ConstantProperties.PF_v1 = row.Cells(8).Value.ToString().ToDoubleFromCurrent().ConvertToSI(su.cinematic_viscosity)
                .ConstantProperties.PF_v2 = row.Cells(9).Value.ToString().ToDoubleFromCurrent().ConvertToSI(su.cinematic_viscosity)
                .ConstantProperties.PR_Volume_Translation_Coefficient = row.Cells(10).Value.ToString().ToDoubleFromCurrent()
                .ConstantProperties.SRK_Volume_Translation_Coefficient = row.Cells(11).Value.ToString().ToDoubleFromCurrent()
            End With
        Next

    End Sub

    Private Sub Form1_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed

        DWSIM.GlobalSettings.Settings.ShutdownPythonEnvironment()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim result = Me.FolderBrowserDialog1.ShowDialog()

        If result = DialogResult.OK Then

            Try
                For Each compound In ccol.Values
                    File.WriteAllText(Path.Combine(Me.FolderBrowserDialog1.SelectedPath, compound.Name + ".json"),
                                      Newtonsoft.Json.JsonConvert.SerializeObject(compound.ConstantProperties, Newtonsoft.Json.Formatting.Indented))
                Next
                MessageBox.Show("Compounds exported successfully.", "Success!", MessageBoxButtons.OK)
            Catch ex As Exception
                MessageBox.Show(frm.GetTranslatedString("Erroaosalvararquivo") + ex.Message.ToString, "DWSIM", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End If

    End Sub

End Class
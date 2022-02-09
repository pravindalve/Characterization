Imports System.IO
Imports Python.Included
Imports Python.Runtime

Module Main

    Sub Main()

        Installer.SetupPython().GetAwaiter().GetResult()
        Installer.InstallWheel(Reflection.Assembly.GetExecutingAssembly(), "numpy-1.20.0-cp37-cp37m-win_amd64.whl").GetAwaiter().GetResult()
        Installer.InstallWheel(Reflection.Assembly.GetExecutingAssembly(), "scipy-1.7.0-cp37-cp37m-win_amd64.whl").GetAwaiter().GetResult()
        Installer.InstallWheel(Reflection.Assembly.GetExecutingAssembly(), "pandas-1.3.0-cp37-cp37m-win_amd64.whl").GetAwaiter().GetResult()
        Installer.InstallWheel(Reflection.Assembly.GetExecutingAssembly(), "pytz-2021.3-py2.py3-none-any.whl").GetAwaiter().GetResult()
        Installer.InstallWheel(Reflection.Assembly.GetExecutingAssembly(), "python_dateutil-2.8.2-py2.py3-none-any.whl").GetAwaiter().GetResult()
        Installer.InstallWheel(Reflection.Assembly.GetExecutingAssembly(), "six-1.16.0-py2.py3-none-any.whl").GetAwaiter().GetResult()
        PythonEngine.Initialize()

        Dim charact As String

        Using filestr As IO.Stream = Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Characterization.characterization.py")
            Using r As New StreamReader(filestr)
                charact = r.ReadToEnd()
            End Using
        End Using

        Using Py.GIL()

            Using scope = Py.CreateScope()

                scope.Exec(charact)

                Dim np As Object = scope.Import("numpy")
                Dim charac1 As Object = scope.Get("characterization")

                Dim SG = 0.809
                Dim x = np.array(New List(Of Double)({2.9, 7.68859754431644, 15.1538050381994, 31.5823352252608, 42.4062967988428, 52.8464767354692, 62.7377406502837, 71.2394217843979, 74.0267929538375, 83.4788183586731, 88.4543077863666, 92.486636460126}))
                Dim y = np.array(New List(Of Double)({15, 65, 100, 150, 200, 250, 300, 350, 370, 450, 500, 550}))

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

                Console.ReadKey()

            End Using

        End Using

    End Sub

End Module

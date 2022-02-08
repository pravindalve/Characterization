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
        Dim sys As Object = PythonEngine.ImportModule("sys")

        Dim charact As String

        Using filestr As IO.Stream = Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Characterization.characterization.py")
            Using r As New StreamReader(filestr)
                charact = r.ReadToEnd()
            End Using
        End Using

        Using Py.GIL()

            Using scope = Py.CreateScope()

                scope.Exec(charact)

                Dim charac1 As Object = scope.Get("characterization")

                Dim result = charac1.characterization()

            End Using

        End Using

    End Sub

End Module

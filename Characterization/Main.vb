Imports System.IO
Imports Python.Included
Imports Python.Runtime

Module Main

    Sub Main()

        Installer.SetupPython().GetAwaiter().GetResult()
        Installer.InstallWheel(Reflection.Assembly.GetExecutingAssembly(), "numpy-1.20.0-cp37-cp37m-win_amd64.whl").GetAwaiter().GetResult()
        Installer.InstallWheel(Reflection.Assembly.GetExecutingAssembly(), "scipy-1.7.0-cp37-cp37m-win_amd64.whl").GetAwaiter().GetResult()
        PythonEngine.Initialize()
        Dim sys As Object = PythonEngine.ImportModule("sys")
        Dim numpy As Object = Py.Import("numpy")
        Dim scipy As Object = Py.Import("scipy")

    End Sub

End Module

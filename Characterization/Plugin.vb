Imports DWSIM.Interfaces

<System.Serializable()> Public Class Plugin

    Implements DWSIM.Interfaces.IUtilityPlugin

    'this variable will reference the active flowsheet in DWSIM, set before plugin's window is opened.

    Public fsheet As DWSIM.Interfaces.IFlowsheet

    Public ReadOnly Property Author() As String = "" Implements DWSIM.Interfaces.IUtilityPlugin.Author

    Public ReadOnly Property ContactInfo() As String = "" Implements DWSIM.Interfaces.IUtilityPlugin.ContactInfo

    Public ReadOnly Property CurrentFlowsheet() As IFlowsheet Implements DWSIM.Interfaces.IUtilityPlugin.CurrentFlowsheet
        Get
            Return fsheet
        End Get
    End Property

    Public ReadOnly Property WebSite() As String = "" Implements DWSIM.Interfaces.IUtilityPlugin.WebSite

    Public ReadOnly Property Description() As String = "" Implements DWSIM.Interfaces.IUtilityPlugin.Description

    Public ReadOnly Property DisplayMode() As DWSIM.Interfaces.IUtilityPlugin.DispMode Implements DWSIM.Interfaces.IUtilityPlugin.DisplayMode
        Get
            Return DWSIM.Interfaces.IUtilityPlugin.DispMode.Dockable
        End Get
    End Property

    Public ReadOnly Property Name() As String Implements DWSIM.Interfaces.IUtilityPlugin.Name
        Get
            Return "Characterization"
        End Get
    End Property

    Public Function SetFlowsheet(form As IFlowsheet) As Boolean Implements DWSIM.Interfaces.IUtilityPlugin.SetFlowsheet
        fsheet = form
        Return True
    End Function

    Public ReadOnly Property UniqueID() As String Implements DWSIM.Interfaces.IUtilityPlugin.UniqueID
        Get
            Return "4746B31E-388E-4DD3-B31C-8851ABFAB8AB"
        End Get
    End Property

    'this is called by DWSIM to open the form, so we need to pass the reference to the flowsheet to the form BEFORE returning it.
    Public ReadOnly Property UtilityForm() As Object Implements DWSIM.Interfaces.IUtilityPlugin.UtilityForm
        Get
            Return Nothing
        End Get
    End Property

End Class

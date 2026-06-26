Namespace My
    Partial Friend Class MyApplication
        Private Sub MyApplication_UnhandledException(sender As Object, e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            Dim logPath As String = IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "PMS_Error.txt")
            Try
                IO.File.WriteAllText(logPath,
                    "Error at: " & DateTime.Now.ToString() & Environment.NewLine &
                    "Message: " & e.Exception.Message & Environment.NewLine &
                    "Stack: " & e.Exception.StackTrace)
            Catch
            End Try
            MessageBox.Show(
                "An unexpected error occurred:" & Environment.NewLine & Environment.NewLine &
                e.Exception.Message & Environment.NewLine & Environment.NewLine &
                "A log has been saved to: " & logPath,
                "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            e.ExitApplication = False
        End Sub
    End Class
End Namespace

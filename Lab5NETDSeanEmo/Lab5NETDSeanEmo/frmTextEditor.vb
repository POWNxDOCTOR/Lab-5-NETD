' Author: Sean Emo 
' Date: April 7, 2020
' Description:
' This program is designed to act like a basic text editor like Notepad. When executed 
' the user can enter text in to the textbox and can then save that text to the file 
' destination that they choose. I was very stressed out about this lab and I didn't really 
' know too much of what I was doing so I had to look up a bunch of resources online. I 
' feel like I did the best considering this new learning system. I really only learn by doing
' so being able to follow along with tutorials or repetition in typing is the way I learn. 

' Added System.IO to allow for reading and writing files, basic file support that a text editor would need. 
Imports System.IO

Public Class frmTextEditor
    '' Variable declaration
    Dim ind As Byte = 0
    Dim path As String
    ' Opens up the form to the center of the screen when the form loads. 
    Private Sub frmTextEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
    End Sub
#Region "ToolStrip Items"

    ' Below allows for the user to click new and a new form appears as a dialog, so it opens up a new
    ' instance of the program. 
    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        Dim newForm As New frmTextEditor
        newForm.ShowDialog()
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        Dim myStream As Stream = Nothing
        ' The filter below opens up in text files for the path because it was required by the program, if we 
        ' change the values to bmp, jpg and replace the variables with the text type you can change the default save 
        ' file type. 
        dbOpenFile.Filter = "Text Files (*.txt)|*.txt|All Files(*.*)|*.*"
        dbOpenFile.FilterIndex = 1
        dbOpenFile.RestoreDirectory = True
        ' The file is attempting to open the openfile dialog box and checking to make sure the file itself is not null. 
        If (dbOpenFile.ShowDialog() = System.Windows.Forms.DialogResult.OK) And (dbOpenFile.FileName.Length > 0) Then
            Try
                ' If that works then the stream opens the file according to the path of the filename and loads the file. 
                myStream = dbOpenFile.OpenFile
                If (myStream IsNot Nothing) Then
                    rtbText.LoadFile(dbOpenFile.FileName, RichTextBoxStreamType.PlainText)
                    path = dbOpenFile.FileName
                End If
                ' If the file cannot be loaded an error message occurs. 
            Catch ex As Exception
                MessageBox.Show("Cannot read file from disk, Original error." & ex.Message)
            Finally
                If (myStream IsNot Nothing) Then
                    ind = 1
                    myStream.Close()
                End If
            End Try
        End If

    End Sub
    ' Save file dialog below. 
    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        ' Repetition of code, I'm glad I could rewrite these a few times to learn how to use them and what they kind of do. 
        ' So a line called save text files appears, it checks to see if the path or file exists and then allows for the saving 
        ' of default txt files, again we could change that by changing the filter line with a different extension type. 
        dbSaveFile.Title = "Save Text Files"
        dbSaveFile.CheckFileExists = True
        dbSaveFile.CheckPathExists = True
        dbSaveFile.DefaultExt = "txt"
        dbSaveFile.Filter = "Text Files (*.txt)|*.txt|All Files(*.*)|*.*"
        dbSaveFile.FilterIndex = 1
        dbSaveFile.RestoreDirectory = True

        ' If the file or path exists it tries to save the textbox to the save file. 
        Try
            rtbText.SaveFile(dbSaveFile.FileName, RichTextBoxStreamType.PlainText)

        Catch ex As Exception
            Call SaveAsToolStripMenuItem_Click(Me, e)
        End Try
    End Sub
    ' This button does pretty much the same thing as above, hence the repititon which is helping me learn. 
    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        ' This file is checking the opposite of the save button above, since you are saving as, you are automatically 
        ' showing the dialog box to choose where and how you want to save, there is no need to check if the file or path
        ' exists. It then brings up the save file dialog box and allows the user to choose where to save the txt file. 
        dbSaveFile.Title = "Save Text Files"
        dbSaveFile.CheckFileExists = False
        dbSaveFile.CheckPathExists = False
        dbSaveFile.DefaultExt = "txt"
        dbSaveFile.Filter = "Text Files (*.txt)|*.txt|All Files(*.*)|*.*"
        dbSaveFile.FilterIndex = 1
        dbSaveFile.RestoreDirectory = True

        If (dbSaveFile.ShowDialog() = DialogResult.OK) Then
            rtbText.SaveFile(dbSaveFile.FileName, RichTextBoxStreamType.PlainText)
        End If
    End Sub
    ' Closes the application when the exit button is hit. 
    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub
    ' Undoes the last thing typed by the user, all of these built in functions helped quite a bit. 
    Private Sub UndoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UndoToolStripMenuItem.Click
        rtbText.Undo()
    End Sub
    ' If a user undid something and made a mistake doing that then the redo button allows them to undo a mistake they undid. 
    Private Sub RedoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RedoToolStripMenuItem.Click
        rtbText.Redo()
    End Sub
    ' Copies the text in the textbox as long as the textbox isn't empty. 
    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        If rtbText.SelectionLength > 0 Then
            rtbText.Copy()
        End If
    End Sub
    ' Cuts the contents of the textbox to the clipboard as long as there is something in the textbox, like the copy button. 
    Private Sub CutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CutToolStripMenuItem.Click
        If rtbText.SelectionLength > 0 Then
            rtbText.Cut()
        End If
    End Sub
    ' pastes whatever has been copied to the clipboard for the user. 
    Private Sub PasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem.Click
        rtbText.Paste()
    End Sub
    ' Sets the textbox to empty if the user hits the delete button. 
    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        Me.rtbText.SelectedText = ""
    End Sub
    ' The select all button highlights all text inside the textbox to be used to cut or copy. 
    Private Sub SelectAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectAllToolStripMenuItem.Click
        rtbText.SelectAll()
    End Sub
    ' This one is a little interesting, I looked this one up to add as well, it is a timestamp that inserts the current date and time 
    ' in to the program whenever the user clicks it, it goes to the internal system clock and gets the current date and time as of when 
    ' it was clicked then displays that in the current format below dd/mm/yyyy hh:mm:ss. 
    Private Sub TimeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TimeToolStripMenuItem.Click
        rtbText.SelectedText = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
    End Sub
    ' This button I saw as well and decided to add it for more functionality, by adding the font dialog box when the user clicks the font 
    ' button, allowing them to change their font, it then sets the font to the textbox. 
    Private Sub FontToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FontToolStripMenuItem.Click
        dbFont.Font = rtbText.Font
        dbFont.ShowDialog()
        rtbText.Font = dbFont.Font
    End Sub
    ' Same as the copy button above, but for the contextual menu
    Private Sub CopyToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem1.Click
        If rtbText.SelectionLength > 0 Then
            rtbText.Copy()
        End If
    End Sub
    ' Same as the cut procedure above, just adding them in for the contextual menu. 
    Private Sub CutToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CutToolStripMenuItem1.Click
        If rtbText.SelectionLength > 0 Then
            rtbText.Cut()
        End If
    End Sub
    ' Pastes the text that has been copied to the clipboard, same as above. 
    Private Sub PasteToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem1.Click
        rtbText.Paste()
    End Sub
    ' Selects all text and highlights it, same as above. 
    Private Sub SelectAllToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SelectAllToolStripMenuItem1.Click
        rtbText.SelectAll()
    End Sub
    ' I saw the help button had an about the user, so I added a message box that pops up with my name, assignment, and course in the 
    ' description. 
    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        MsgBox("NETD 2202" & vbCrLf & "Lab 5" & vbCrLf & "Sean Emo" & vbCrLf)
    End Sub
End Class
#End Region
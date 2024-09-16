'****************************************************************************
'    TDocKiller
'    Copyright (C) 2023-2024  CJH
'
'    This program is free software: you can redistribute it and/or modify
'    it under the terms of the GNU General Public License as published by
'    the Free Software Foundation, either version 3 of the License, or
'    (at your option) any later version.
'
'    This program is distributed in the hope that it will be useful,
'    but WITHOUT ANY WARRANTY; without even the implied warranty of
'    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'    GNU General Public License for more details.
'
'    You should have received a copy of the GNU General Public License
'    along with this program.  If not, see <http://www.gnu.org/licenses/>.
'****************************************************************************
'/*****************************************************\
'*                                                     *
'*     TDocKiller - Form2.vb                           *
'*                                                     *
'*     Copyright (c) CJH.                              *
'*                                                     *
'*     Settings Form.                                  *
'*                                                     *
'\*****************************************************/
Imports System.Runtime.InteropServices
Imports Microsoft.Win32
Public Class Form2
    <DllImport("dwmapi.dll")> _
    Public Shared Function DwmSetWindowAttribute(ByVal hwnd As IntPtr, ByVal attr As DwmWindowAttribute, ByRef attrValue As Integer, ByVal attrSize As Integer) As Integer
    End Function
    'Public Shared Function EnableDarkModeForWindow(ByVal hWnd As IntPtr, ByVal enable As Boolean) As Boolean
    '    Dim darkMode As Integer
    '    darkMode = enable
    '    Dim hr As Integer
    '    Dim i As Integer
    '    'hr = DwmSetWindowAttribute(hWnd, DwmWindowAttribute.UseImmersiveDarkMode, darkMode, sizeof(i))
    '    Return hr >= 0
    'End Function
    Public Shared Function EnableDarkModeForWindow(ByVal hWnd As IntPtr, ByVal enable As Boolean) As Boolean
        Dim attrValue As Integer = If(enable, 1, 0)
        Return (Form2.DwmSetWindowAttribute(hWnd, DwmWindowAttribute.UseImmersiveDarkMode, attrValue, 4) >= 0)
    End Function

    Public Enum DwmWindowAttribute As UInt32
        NCRenderingEnabled = 1
        NCRenderingPolicy
        TransitionsForceDisabled
        AllowNCPaint
        CaptionButtonBounds
        NonClientRtlLayout
        ForceIconicRepresentation
        Flip3DPolicy
        ExtendedFrameBounds
        HasIconicBitmap
        DisallowPeek
        ExcludedFromPeek
        Cloak
        Cloaked
        FreezeRepresentation
        PassiveUpdateMode
        UseHostBackdropBrush
        UseImmersiveDarkMode = 20
        WindowCornerPreference = 33
        BorderColor
        CaptionColor
        TextColor
        VisibleFrameBorderThickness
        SystemBackdropType
        Last
    End Enum

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        On Error GoTo errcode
        If TextBox1.Text = "" Then
            MsgBox("隐藏时间不能为空！", MsgBoxStyle.Critical, "错误")
        ElseIf TextBox1.Text = "0" Then
            MsgBox("隐藏时间不能为0！", MsgBoxStyle.Critical, "错误")
        Else
            If ComboBox1.SelectedIndex = 0 Then '秒
                Form1.Timer2.Interval = TextBox1.Text * 1000
                Form1.Timer1.Enabled = False
                Form1.NotifyIcon1.Visible = True
                Form1.NotifyIcon1.ShowBalloonTip(7000, "一键关闭课件小工具", "一键关闭课件小工具当前已隐藏到系统托盘，双击托盘图标或在设定的时间（" & TextBox1.Text & "秒）之后重新显示。", ToolTipIcon.Info)
                Form1.Timer2.Enabled = True
                Form1.Hide()
                Me.Close()
            ElseIf ComboBox1.SelectedIndex = 1 Then '分
                Form1.Timer2.Interval = TextBox1.Text * 1000 * 60
                Form1.Timer1.Enabled = False
                Form1.NotifyIcon1.Visible = True
                Form1.NotifyIcon1.ShowBalloonTip(7000, "一键关闭课件小工具", "一键关闭课件小工具当前已隐藏到系统托盘，双击托盘图标或在设定的时间（" & TextBox1.Text & "分钟）之后重新显示。", ToolTipIcon.Info)
                Form1.Timer2.Enabled = True
                Form1.Hide()
                Me.Close()
            ElseIf ComboBox1.SelectedIndex = 2 Then '时
                Form1.Timer2.Interval = TextBox1.Text * 1000 * 60 * 60
                Form1.Timer1.Enabled = False
                Form1.NotifyIcon1.Visible = True
                Form1.NotifyIcon1.ShowBalloonTip(7000, "一键关闭课件小工具", "一键关闭课件小工具当前已隐藏到系统托盘，双击托盘图标或在设定的时间（" & TextBox1.Text & "小时）之后重新显示。", ToolTipIcon.Info)
                Form1.Timer2.Enabled = True
                Form1.Hide()
                Me.Close()
            End If
        End If
        Exit Sub
errcode:
        MsgBox(Err.Description, MsgBoxStyle.Critical, "错误")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ComboBox1.SelectedIndex = 0
        ComboBox1.SelectedText = "秒"

        If Form1.TopMost = True Then
            Me.CheckBox1.Checked = True
        Else
            Me.CheckBox1.Checked = False
        End If
        If Form1.UseMoveV = 1 Then
            Me.CheckBox2.Checked = True
        Else
            Me.CheckBox2.Checked = False
        End If

        Dim disi As Graphics = Me.CreateGraphics()
        'TextBox2.Height = disi.DpiX * 0.01 * 139
        Label8.Width = Me.Width - disi.DpiX * 0.01 * 54

        Call formatcolorcurset()

        If Form1.UnSupportDarkSys = 1 Then
            If Form1.appcolor > 0 Then
                ComboBox2.SelectedIndex = Form1.appcolor - 1
            End If
            If Form1.appcolor = 1 Then
                ComboBox2.SelectedText = "浅色"
            ElseIf Form1.appcolor = 2 Then
                ComboBox2.SelectedText = "深色"
            End If
        Else
            ComboBox2.SelectedIndex = Form1.appcolor
            If Form1.appcolor = 0 Then
                ComboBox2.SelectedText = "跟随系统"
            ElseIf Form1.appcolor = 1 Then
                ComboBox2.SelectedText = "浅色"
            ElseIf Form1.appcolor = 2 Then
                ComboBox2.SelectedText = "深色"
            End If
        End If
        'Dim ver1 As String
        'Dim ver2 As String
        'ver1 = My.Application.Info.Version.Major.ToString & "." & My.Application.Info.Version.Minor.ToString & "." & My.Application.Info.Version.Build.ToString
        'ver2 = My.Application.Info.Version.Revision.ToString
        Label1.Text = "一键关闭课件小工具 版本：" & My.Application.Info.Version.ToString & vbCrLf & "版权所有 © 2023-2024 CJH。"
    End Sub

    Sub formatcolorcurset()
        If Form1.crmd = 0 Then
            EnableDarkModeForWindow(Me.Handle, True)
            Me.BackColor = Color.FromArgb(32, 32, 32)
            Me.ForeColor = Color.White
            Me.Button1.BackColor = Color.FromArgb(32, 32, 32)
            Me.Button1.ForeColor = Color.White
            Me.Button2.BackColor = Color.FromArgb(32, 32, 32)
            Me.Button2.ForeColor = Color.White
            Me.Button4.BackColor = Color.FromArgb(32, 32, 32)
            Me.Button4.ForeColor = Color.White
            Me.TextBox1.BackColor = Color.FromArgb(32, 32, 32)
            Me.TextBox1.ForeColor = Color.White
            'Me.TextBox2.BackColor = Color.FromArgb(32, 32, 32)
            'Me.TextBox2.ForeColor = Color.White
            Me.ComboBox1.BackColor = Color.FromArgb(32, 32, 32)
            Me.ComboBox1.ForeColor = Color.White
            Me.ComboBox2.BackColor = Color.FromArgb(32, 32, 32)
            Me.ComboBox2.ForeColor = Color.White
            Me.CheckBox1.BackColor = Color.FromArgb(32, 32, 32)
            Me.CheckBox1.ForeColor = Color.White
            Me.CheckBox2.BackColor = Color.FromArgb(32, 32, 32)
            Me.CheckBox2.ForeColor = Color.White
        Else
            EnableDarkModeForWindow(Me.Handle, False)
            Me.BackColor = Color.White
            Me.ForeColor = Color.Black
            Me.Button1.BackColor = Color.Transparent
            Me.Button1.ForeColor = Color.Black
            Me.Button2.BackColor = Color.Transparent
            Me.Button2.ForeColor = Color.Black
            Me.Button4.BackColor = Color.Transparent
            Me.Button4.ForeColor = Color.Black
            Me.TextBox1.BackColor = Color.White
            Me.TextBox1.ForeColor = Color.Black
            'Me.TextBox2.BackColor = Color.White
            'Me.TextBox2.ForeColor = Color.Black
            Me.ComboBox1.BackColor = Color.White
            Me.ComboBox1.ForeColor = Color.Black
            Me.ComboBox2.BackColor = Color.White
            Me.ComboBox2.ForeColor = Color.Black
            Me.CheckBox1.BackColor = Color.White
            Me.CheckBox1.ForeColor = Color.Black
            Me.CheckBox2.BackColor = Color.White
            Me.CheckBox2.ForeColor = Color.Black
        End If
    End Sub
    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        Dim strlimit As String
        strlimit = "0123456789"
        Dim keychar As Char = e.KeyChar
        If InStr(strlimit, keychar) <> 0 Or e.KeyChar = Microsoft.VisualBasic.ChrW(8) Then
            'If keychar = "." And InStr(TextBox1.Text, keychar) <> 0 Then
            'e.Handled = True
            'Else
            e.Handled = False
            'End If
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = False Then
            'CheckBox1.Checked = False
            Form1.TopMost = False
            If Form1.UnSaveData = 0 Then
                AddReg("Software\CJH\TDocKiller\Settings", "AllowTopMost", 0, RegistryValueKind.DWord, "HKCU")
            End If
        Else
            'CheckBox1.Checked = True
            Form1.TopMost = True
            If Form1.UnSaveData = 0 Then
                AddReg("Software\CJH\TDocKiller\Settings", "AllowTopMost", 1, RegistryValueKind.DWord, "HKCU")
            End If
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = False Then
            'CheckBox2.Checked = False
            Form1.UseMoveV = 0
            If Form1.UnSaveData = 0 Then
                AddReg("Software\CJH\TDocKiller\Settings", "EnableDrag", 0, RegistryValueKind.DWord, "HKCU")
            End If

        Else
            'CheckBox2.Checked = True
            Form1.UseMoveV = 1
            If Form1.UnSaveData = 0 Then
                AddReg("Software\CJH\TDocKiller\Settings", "EnableDrag", 1, RegistryValueKind.DWord, "HKCU")
            End If

        End If
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        If MessageBox.Show("确定关闭一键关闭课件小工具吗？", "一键关闭课件小工具 - 提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            End
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox1.Click
        Label1.Text = "一键关闭课件小工具 版本：" & My.Application.Info.Version.ToString & vbCrLf & "版权所有 © 2023-2024 CJH。" & vbCrLf & "TDocKiller /eggs"
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.SelectedIndex = 0 Then
            If Form1.UnSupportDarkSys = 1 Then
                If Form1.UnSaveData = 0 Then
                    AddReg("Software\CJH\TDocKiller\Settings", "ColorMode", 1, Microsoft.Win32.RegistryValueKind.DWord, "HKCU")
                End If

                Form1.crmd = 1 'Light
                Form1.appcolor = 1
                Call Me.formatcolorcurset()
                Call Form1.formatcolorcur()
            Else
                Form1.appcolor = 0
                If Form1.UnSaveData = 0 Then
                    AddReg("Software\CJH\TDocKiller\Settings", "ColorMode", 0, Microsoft.Win32.RegistryValueKind.DWord, "HKCU")
                End If

                'Get System Color
                Dim regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", True)
                Dim sysacr As Integer
                If (Not regkey Is Nothing) Then
                    'If (If((regkey.GetValue("") Is Nothing), Nothing, regkey.GetValue("").ToString) <> "AppsUseLightTheme") Then
                    'End If
                    sysacr = regkey.GetValue("AppsUseLightTheme", -1)
                    If sysacr = 0 Then
                        Form1.crmd = 0
                    ElseIf sysacr = 1 Then
                        Form1.crmd = 1
                    Else
                        Form1.crmd = 1
                    End If
                Else
                    Form1.crmd = 1
                End If
                regkey.Close()
                Call Me.formatcolorcurset()
                Call Form1.formatcolorcur()
            End If
        ElseIf ComboBox2.SelectedIndex = 1 Then
            If Form1.UnSupportDarkSys = 1 Then
                If Form1.UnSaveData = 0 Then
                    AddReg("Software\CJH\TDocKiller\Settings", "ColorMode", 2, Microsoft.Win32.RegistryValueKind.DWord, "HKCU")
                End If

                Form1.crmd = 0 'Dark
                Form1.appcolor = 2
                Call Me.formatcolorcurset()
                Call Form1.formatcolorcur()
            Else
                If Form1.UnSaveData = 0 Then
                    AddReg("Software\CJH\TDocKiller\Settings", "ColorMode", 1, Microsoft.Win32.RegistryValueKind.DWord, "HKCU")
                End If

                Form1.crmd = 1 'Light
                Form1.appcolor = 1

                Call Me.formatcolorcurset()
                Call Form1.formatcolorcur()

            End If
        ElseIf ComboBox2.SelectedIndex = 2 Then
            If Form1.UnSaveData = 0 Then
                AddReg("Software\CJH\TDocKiller\Settings", "ColorMode", 2, Microsoft.Win32.RegistryValueKind.DWord, "HKCU")
            End If
            Form1.crmd = 0 'Dark
            Form1.appcolor = 2
            Call Me.formatcolorcurset()
            Call Form1.formatcolorcur()
        End If
    End Sub

    Private Sub LinkLabel4_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel4.LinkClicked
        'GPLForm.TextBox1.ScrollBars = ScrollBars.Both
        GPLForm.TextBox1.Text = My.Resources.GPL3
        'GPLForm.TextBox1.WordWrap = False
        'GPLForm.TextBox1.Font = New System.Drawing.Font("宋体", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        GPLForm.Text = "GNU General Public License 3"
        GPLForm.ShowDialog()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        System.Diagnostics.Process.Start("https://github.com/cjhdevact/TDocKiller")
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        System.Diagnostics.Process.Start("https://github.com/cjhdevact/TDocKiller/issues")
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        Form1.NotifyIcon1.Visible = True
        Form1.NotifyIcon1.ShowBalloonTip(7000, "一键关闭课件小工具", "一键关闭课件小工具当前已隐藏到系统托盘，双击托盘图标重新显示。", ToolTipIcon.Info)
        Form1.Hide()
        Me.Close()
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        If (MessageBox.Show("确定重启一键关闭课件小工具吗？", "一键关闭课件小工具", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes) Then
            System.Diagnostics.Process.Start(Application.ExecutablePath)
            End
        End If
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        'GPLForm.TextBox1.ScrollBars = ScrollBars.Vertical
        GPLForm.TextBox1.Text = My.Resources.DocText
        'GPLForm.TextBox1.WordWrap = True
        'GPLForm.TextBox1.Font = New System.Drawing.Font("微软雅黑", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        GPLForm.Text = "帮助"
        GPLForm.ShowDialog()
    End Sub
End Class
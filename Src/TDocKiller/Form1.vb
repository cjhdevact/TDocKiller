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
'*     TDocKiller - Form1.vb                           *
'*                                                     *
'*     Copyright (c) CJH.                              *
'*                                                     *
'*     Toolbar UI.                                     *
'*                                                     *
'\*****************************************************/

Imports Microsoft.Win32
Imports System.Runtime.InteropServices

Public Class Form1
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
        Return (Form1.DwmSetWindowAttribute(hWnd, DwmWindowAttribute.UseImmersiveDarkMode, attrValue, 4) >= 0)
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

    Public a As New System.Drawing.Point
    Public crmd As Integer ' 0=Dark 1=Light
    Public appcolor As Integer ' 0= With System 1= Dark 2= Light
    Public CloseStateV As Integer
    Public RenS As Integer
    Public CurState As Integer
    Public MovedV As Integer
    Public UseMoveV As Integer
    Public TargetNames(78) As String
    Public UnSupportDarkSys As Integer
    Public UnSaveData As Integer
    Public DisbFuState As Integer
    Public ShowModeTips As Integer
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If MovedV <> 1 Then
            If Me.Location <> a Then
                Me.Location = a
            End If
        End If
    End Sub
    'API移动窗体
    Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Boolean
    Declare Function ReleaseCapture Lib "user32" Alias "ReleaseCapture" () As Boolean
    Const WM_SYSCOMMAND = &H112
    Const SC_MOVE = &HF010&
    Const HTCAPTION = 2
    Private Sub Form1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        If UseMoveV = 1 Then
            ReleaseCapture()
            SendMessage(Me.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0)
            MovedV = 1
        End If
    End Sub
    Private Sub Button1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button1.MouseDown
        If UseMoveV = 1 Then
            Dim a As New System.Drawing.Point
            a.X = Me.Location.X
            a.Y = Me.Location.Y
            ReleaseCapture()
            SendMessage(Me.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0)
            MovedV = 1
            If Me.Location.X = a.X And Me.Location.Y = a.Y Then
                Call Button1_Click(sender, e)
            End If
        End If
    End Sub
    Sub disbfu()
        DisbFuState = 1
        Form2.Label8.Text = "部分功能由于被管理员禁用而无法使用。"
        Form2.CheckBox1.Enabled = False
        Form2.CheckBox2.Enabled = False
        Form2.ComboBox2.Enabled = False
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '////////////////////////////////////////////////////////////////////////////////////
        '//
        '//  禁用功能策略注册表读取
        '//
        '////////////////////////////////////////////////////////////////////////////////////
        DisbFuState = 0
        UnSaveData = 0
        ShowModeTips = 1

        Dim cdisbfu As Integer = 0
        Dim cdisbfut As Integer = 0
        Dim unsavefut As Integer = 0
        Try

            Dim plkeycr As RegistryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\CJH\Policies\TDocKiller", True)

            Dim disfucrt As Integer = -1
            If (Not plkeycr Is Nothing) Then
                disfucrt = plkeycr.GetValue("DisableFeaturesTip", -1)
                If disfucrt = 1 Then
                    Form2.Label8.Visible = False
                    ShowModeTips = 0
                    cdisbfut = 1
                ElseIf disfucrt = 0 Then
                    Form2.Label8.Visible = True
                    ShowModeTips = 1
                    cdisbfut = 0
                End If
            End If

            Dim disfucr As Integer
            If (Not plkeycr Is Nothing) Then
                disfucr = plkeycr.GetValue("DisableFeatures", -1)
                If disfucr = 1 Then
                    Call disbfu()
                    cdisbfu = 1
                End If
            End If


            Dim unsavecfgcr As Integer
            If (Not plkeycr Is Nothing) Then
                unsavecfgcr = plkeycr.GetValue("NoSaveProfile", -1)
                If unsavecfgcr = 1 Then
                    UnSaveData = 1
                    If DisbFuState = 1 Then
                        Form2.Label8.Text = "由于策略设置，你的更改将不会被保存。部分功能已被禁用。"
                    Else
                        Form2.Label8.Text = "由于策略设置，你的更改将不会被保存。"
                    End If
                    unsavefut = 1
                End If
            End If

            If (Not plkeycr Is Nothing) Then
                plkeycr.Close()
            End If
        Catch ex As Exception
        End Try

        Try
            Dim plkey As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\CJH\Policies\TDocKiller", True)
            Dim disfu As Integer

            If cdisbfut = 0 Then
                Dim disfut As Integer = -1
                If (Not plkey Is Nothing) Then
                    disfut = plkey.GetValue("DisableFeaturesTip", -1)
                    If disfut = 1 Then
                        Form2.Label8.Visible = False
                        ShowModeTips = 0
                    ElseIf disfut = 0 Then
                        Form2.Label8.Visible = True
                        ShowModeTips = 1
                    End If
                End If
            End If

            If cdisbfu = 0 Then
                If (Not plkey Is Nothing) Then
                    disfu = plkey.GetValue("DisableFeatures", -1)
                    If disfu = 1 Then
                        Call disbfu()
                    End If
                End If
            End If

            If unsavefut = 0 Then
                Dim unsavecfg As Integer
                If (Not plkey Is Nothing) Then
                    unsavecfg = plkey.GetValue("NoSaveProfile", -1)
                    If unsavecfg = 1 Then
                        UnSaveData = 1
                        If DisbFuState = 1 Then
                            Form2.Label8.Text = "由于策略设置，你的更改将不会被保存。部分功能已被禁用。"
                        Else
                            Form2.Label8.Text = "由于策略设置，你的更改将不会被保存。"
                        End If
                    End If
                End If
            End If


            If (Not plkey Is Nothing) Then
                plkey.Close()
            End If

        Catch ex As Exception
        End Try

        Try
            If Command().ToLower = "/nosaveprofile" Then
                If DisbFuState = 1 Then
                    Form2.Label8.Text = "当前你的更改将不会被保存。部分功能已被禁用。"
                Else
                    Form2.Label8.Text = "当前你的更改将不会被保存。"
                End If
                UnSaveData = 1
            End If
        Catch ex As Exception
        End Try

        If UnSaveData = 0 And DisbFuState = 0 Then
            Form2.Label8.Visible = False
            ShowModeTips = 0
        End If

        Try
            AddKey("Software\CJH", "HKCU")
            AddKey("Software\CJH\TDocKiller", "HKCU")
            AddKey("Software\CJH\TDocKiller\Settings", "HKCU")
        Catch ex As Exception
        End Try
        Dim mykey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\CJH\TDocKiller\Settings", True)
        Dim myv As Integer
        Try
            '////////////////////////////////////////////////////////////////////////////////////
            '//
            '//  颜色主题注册表读取
            '//
            '////////////////////////////////////////////////////////////////////////////////////
            If (Not mykey Is Nothing) Then
                'If (If((regkey.GetValue("") Is Nothing), Nothing, regkey.GetValue("").ToString) <> "AppsUseLightTheme") Then
                'End If
                myv = mykey.GetValue("ColorMode", -1)
                If myv = 0 Then
                    appcolor = 0
                ElseIf myv = -1 Then
                    appcolor = 0

                    AddReg("Software\CJH\TDocKiller\Settings", "ColorMode", 0, Microsoft.Win32.RegistryValueKind.DWord, "HKCU")


                ElseIf myv = 1 Then
                    appcolor = 1
                ElseIf myv = 2 Then
                    appcolor = 2
                End If
            Else
                appcolor = 0

                AddReg("Software\CJH\TDocKiller\Settings", "ColorMode", 0, Microsoft.Win32.RegistryValueKind.DWord, "HKCU")
            End If
            '////////////////////////////////////////////////////////////////////////////////////
            '//
            '//  系统颜色读取注册表读取
            '//
            '////////////////////////////////////////////////////////////////////////////////////
            Try
                'Get System Color
                Dim regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", True)
                Dim sysacr As Integer
                Try
                    If (Not regkey Is Nothing) Then
                        'If (If((regkey.GetValue("") Is Nothing), Nothing, regkey.GetValue("").ToString) <> "AppsUseLightTheme") Then
                        'End If
                        sysacr = regkey.GetValue("AppsUseLightTheme", -1)
                    Else
                        UnSupportDarkSys = 1
                    End If
                Catch ex As Exception
                    UnSupportDarkSys = 1
                End Try
                If appcolor = 0 Then
                    If (Not regkey Is Nothing) Then
                        'If (If((regkey.GetValue("") Is Nothing), Nothing, regkey.GetValue("").ToString) <> "AppsUseLightTheme") Then
                        'End If
                        If sysacr = 0 Then
                            UnSupportDarkSys = 0
                            crmd = 0
                        ElseIf sysacr = 1 Then
                            UnSupportDarkSys = 0
                            crmd = 1
                        Else
                            UnSupportDarkSys = 1
                            crmd = 1
                        End If
                    Else
                        UnSupportDarkSys = 1
                        crmd = 1
                    End If
                ElseIf appcolor = 1 Then
                    If (Not regkey Is Nothing) Then
                        If sysacr = 0 Then
                            UnSupportDarkSys = 0
                        ElseIf sysacr = 1 Then
                            UnSupportDarkSys = 0
                        Else
                            UnSupportDarkSys = 1
                        End If
                    Else
                        UnSupportDarkSys = 1
                    End If
                    crmd = 1 'Light
                ElseIf appcolor = 2 Then
                    If (Not regkey Is Nothing) Then
                        If sysacr = 0 Then
                            UnSupportDarkSys = 0
                        ElseIf sysacr = 1 Then
                            UnSupportDarkSys = 0
                        Else
                            UnSupportDarkSys = 1
                        End If
                    Else
                        UnSupportDarkSys = 1
                    End If
                    crmd = 0 'Dark
                End If

                regkey.Close()
            Catch ex As Exception
            End Try


            If UnSupportDarkSys = 1 Then
                If appcolor = 0 Then
                    appcolor = 1
                    AddReg("Software\CJH\TDocKiller\Settings", "ColorMode", 1, Microsoft.Win32.RegistryValueKind.DWord, "HKCU")
                    Form2.ComboBox2.SelectedIndex = 0
                ElseIf appcolor = 1 Then
                    Form2.ComboBox2.SelectedIndex = 0
                ElseIf appcolor = 2 Then
                    Form2.ComboBox2.SelectedIndex = 1
                End If

                Form2.ComboBox2.Items.Clear()
                Form2.ComboBox2.Items.AddRange(New Object() {"浅色", "深色"})
            End If

            AddHandler SystemEvents.UserPreferenceChanged, AddressOf SystemEvents_UserPreferenceChanged

            '////////////////////////////////////////////////////////////////////////////////////
            '//
            '//  是否启用拖放注册表读取
            '//
            '////////////////////////////////////////////////////////////////////////////////////

            If (Not mykey Is Nothing) Then
                Me.UseMoveV = mykey.GetValue("EnableDrag", -1)
                If Me.UseMoveV = -1 Then
                    RegKeyModule.AddReg("Software\CJH\TDocKiller\Settings", "EnableDrag", 1, RegistryValueKind.DWord, "HKCU")
                    Me.UseMoveV = 1
                ElseIf Me.UseMoveV > 1 Then
                    RegKeyModule.AddReg("Software\CJH\TDocKiller\Settings", "EnableDrag", 1, RegistryValueKind.DWord, "HKCU")
                    Me.UseMoveV = 1
                End If
            Else
                RegKeyModule.AddReg("Software\CJH\TDocKiller\Settings", "EnableDrag", 1, RegistryValueKind.DWord, "HKCU")
                Me.UseMoveV = 1
            End If

            '////////////////////////////////////////////////////////////////////////////////////
            '//
            '//  顶置注册表读取
            '//
            '////////////////////////////////////////////////////////////////////////////////////

            Dim UseTop As Integer
            If (Not mykey Is Nothing) Then
                UseTop = mykey.GetValue("AllowTopMost", -1)
                If UseTop = -1 Then
                    RegKeyModule.AddReg("Software\CJH\TDocKiller\Settings", "AllowTopMost", 1, RegistryValueKind.DWord, "HKCU")
                    Me.TopMost = True
                ElseIf UseTop = 0 Then
                    Me.TopMost = False
                ElseIf UseTop = 1 Then
                    Me.TopMost = True
                ElseIf Me.UseMoveV > 1 Then
                    RegKeyModule.AddReg("Software\CJH\TDocKiller\Settings", "AllowTopMost", 1, RegistryValueKind.DWord, "HKCU")
                    Me.TopMost = True
                End If
            Else
                RegKeyModule.AddReg("Software\CJH\TDocKiller\Settings", "AllowTopMost", 1, RegistryValueKind.DWord, "HKCU")
                Me.TopMost = True
            End If

        Catch ex As Exception
            MsgBox("读取注册表设置发生错误。" & vbCrLf & ex.Message, MsgBoxStyle.Critical, "错误")
        End Try


        Call formatcolorcur()
        Call Form2.formatcolorcurset()
        Call GPLForm.formatcolorcursetmsg()

        MovedV = 0

        TargetNames(0) = "WINWORD"
        TargetNames(1) = "EXCEL"
        TargetNames(2) = "POWERPNT"
        TargetNames(3) = "wps"
        TargetNames(4) = "et"
        TargetNames(5) = "wpp"
        TargetNames(6) = "wpspdf"
        TargetNames(7) = "wpsoffice"
        TargetNames(8) = "msedge"
        TargetNames(9) = "chrome"
        TargetNames(10) = "firefox"
        TargetNames(11) = "EasiNote"
        TargetNames(12) = "EasiCamera"
        TargetNames(13) = "Wechat"
        TargetNames(14) = "db"
        TargetNames(15) = "Cbox"
        TargetNames(16) = "qyplayer"
        TargetNames(17) = "QQLive"
        TargetNames(18) = "kugou"
        TargetNames(19) = "kuwomusic"
        TargetNames(20) = "wpspic"
        TargetNames(21) = "iexplore"
        TargetNames(22) = "PotPlayer"
        TargetNames(23) = "PotPlayerMini"
        TargetNames(24) = "PhotosApp"
        TargetNames(25) = "PhotosService"
        TargetNames(26) = "Microsoft.Photos"
        TargetNames(27) = "Microsoft.Media.Player"
        TargetNames(28) = "Groove"
        TargetNames(29) = "WindowsCamera"
        TargetNames(30) = "SoundRec"
        TargetNames(31) = "CalculatorApp"
        TargetNames(32) = "calc"
        TargetNames(33) = "notepad"
        TargetNames(34) = "rundll32"
        TargetNames(35) = "dllhost"
        TargetNames(36) = "mspaint"
        TargetNames(37) = "wmplayer"
        TargetNames(38) = "Video.UI"
        TargetNames(39) = "SnippingTool"
        TargetNames(40) = "PotPlayerMini64"
        TargetNames(41) = "360chrome"
        TargetNames(42) = "360se"
        TargetNames(43) = "winrar"
        TargetNames(44) = "winzip"
        TargetNames(45) = "7z"
        TargetNames(46) = "7zFM"
        TargetNames(47) = "bandzip"
        TargetNames(48) = "theworld"
        TargetNames(49) = "liebao"
        TargetNames(50) = "qingniao"
        TargetNames(51) = "Twinkstar"
        TargetNames(52) = "UCBrowser"
        TargetNames(53) = "UCService"
        TargetNames(54) = "2345Explorer"
        TargetNames(55) = "quark"
        TargetNames(56) = "iexplore"
        TargetNames(57) = "QQBrowser"
        TargetNames(58) = "Chromium"
        TargetNames(59) = "SeewoBrowser"
        TargetNames(60) = "360chromex"
        TargetNames(61) = "360aibrowser"
        TargetNames(62) = "QQPlayer"
        TargetNames(63) = "baofeng"
        TargetNames(64) = "SLBrowser"
        TargetNames(65) = "SLB"
        TargetNames(66) = "QQ"
        TargetNames(67) = "StormPlayer"
        TargetNames(68) = "SogouExplorer"
        TargetNames(69) = "NimoNavigator"
        TargetNames(70) = "CamShow"
        TargetNames(71) = "ScreenBoard"
        TargetNames(72) = "Nimo"
        TargetNames(73) = "HiteCamera"
        TargetNames(74) = "HitePai"
        TargetNames(75) = "nanazip"
        TargetNames(76) = "haozip"
        TargetNames(77) = "360zip"
        TargetNames(78) = "MicrosoftEdge"

        Dim disi As Graphics = Me.CreateGraphics()
        Timer1.Enabled = True

        If Command().ToLower <> "" Then
            If Command().ToLower = "/lefttopbar" Or Command().ToLower = "/righttopbar" Or Command().ToLower = "/leftbottombar" Or Command().ToLower = "/rightbottombar" Then
                If disi.DpiX <= 96 Then
                    Me.Height = 29
                    Me.Width = 118
                Else
                    Me.Height = 29 * disi.DpiY * 0.01 '* 1.05
                    Me.Width = 118 * disi.DpiX * 0.01 '* 1.05
                End If
            ElseIf Command().ToLower = "/topbar" Then
                If disi.DpiX <= 96 Then
                    Me.Height = 29
                    Me.Width = 118
                Else
                    Me.Height = 29 * disi.DpiY * 0.01 '* 1.05
                    Me.Width = 118 * disi.DpiX * 0.01 '* 1.05
                End If
            ElseIf Command().ToLower = "/bottombar" Then
                If disi.DpiX <= 96 Then
                    Me.Height = 29
                    Me.Width = 118
                Else
                    Me.Height = 29 * disi.DpiY * 0.01 '* 1.05
                    Me.Width = 118 * disi.DpiX * 0.01 '* 1.05
                End If
            Else
                If disi.DpiX <= 96 Then
                    Me.Height = 118
                    Me.Width = 29
                Else
                    Me.Height = 118 * disi.DpiY * 0.01 '* 1.05
                    Me.Width = 29 * disi.DpiX * 0.01 '* 1.05
                End If
            End If
        Else
            If disi.DpiX <= 96 Then
                Me.Height = 118
                Me.Width = 29
            Else
                Me.Height = 118 * disi.DpiY * 0.01 '* 1.05
                Me.Width = 29 * disi.DpiX * 0.01 '* 1.05
            End If
        End If

        'Me.Height = Label1.Height
        'Me.Width = Label1.Width
        'a.X = (System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width - Me.Width) - 3 * disi.DpiX * 0.01
        If Command().ToLower <> "" Then
            If Command().ToLower = "/leftbar" Then
                a.X = 3 * disi.DpiX * 0.01
                a.Y = (System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height - Me.Height) / 2 - 100 * disi.DpiX * 0.01
            ElseIf Command().ToLower = "/topbar" Then
                a.X = (System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width - Me.Width) / 2
                a.Y = 5 * disi.DpiX * 0.01
            ElseIf Command().ToLower = "/bottombar" Then
                a.X = (System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width - Me.Width) / 2
                a.Y = (System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height - Me.Height) - 45 * disi.DpiX * 0.01
            ElseIf Command().ToLower = "/lefttopbar" Then
                a.X = 3 * disi.DpiX * 0.01
                a.Y = 3 * disi.DpiX * 0.01
            ElseIf Command().ToLower = "/righttopbar" Then
                a.X = (System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width - Me.Width) - 3 * disi.DpiX * 0.01
                a.Y = 3 * disi.DpiX * 0.01
            ElseIf Command().ToLower = "/leftbottombar" Then
                a.X = 3 * disi.DpiX * 0.01
                a.Y = (System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height - Me.Height) - 45 * disi.DpiX * 0.01
            ElseIf Command().ToLower = "/rightbottombar" Then
                a.X = (System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width - Me.Width) - 3 * disi.DpiX * 0.01
                a.Y = (System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height - Me.Height) - 45 * disi.DpiX * 0.01
            ElseIf Command().ToLower = "/rightbar" Then
                a.X = (System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width - Me.Width) - 3 * disi.DpiX * 0.01
                a.Y = (System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height - Me.Height) / 2 - 100 * disi.DpiX * 0.01
            Else
                a.X = (System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width - Me.Width) - 3 * disi.DpiX * 0.01
                a.Y = (System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height - Me.Height) / 2 - 100 * disi.DpiX * 0.01
            End If
        Else
            a.X = (System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width - Me.Width) - 3 * disi.DpiX * 0.01
            a.Y = (System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height - Me.Height) / 2 - 100 * disi.DpiX * 0.01
        End If
        If Command().ToLower = "/lefttopbar" Or Command().ToLower = "/righttopbar" Or Command().ToLower = "/leftbottombar" Or Command().ToLower = "/rightbottombar" Or Command() = "/topbar" Or Command() = "/bottombar" Then
            Button1.Text = "关闭课件"
        Else
            'Button1.Text = "关" & vbCrLf & "闭" & vbCrLf & "课" & vbCrLf & "件"
            Button1.Text = "关闭课件"
        End If
        If Command().ToLower <> "" Then
            If Command().ToLower = "/eggs" Then
                MessageBox.Show("一键关闭课件小工具 " & My.Application.Info.Version.ToString & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & "   ++++++++++++作者 CJH++++++++++++   " & vbCrLf & vbCrLf & "   本工具是为方便让下课后快速关闭课件设计的。主要为希沃一体机准备。" & vbCrLf & "   本程序由时间小工具到电源小工具最后修改而来" & vbCrLf & vbCrLf & "   Have a good time!" & vbCrLf & vbCrLf & "   -+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-   ", "一键关闭课件小工具 - Eggs", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

        Me.Location = a
        CloseStateV = 0
        RenS = 0
        CurState = 0
        'ContextMenuStrip1.Font = New Font(ContextMenuStrip1.Font.Name, 8.25F * 96.0F / CreateGraphics().DpiX, ContextMenuStrip1.Font.Style, ContextMenuStrip1.Font.Unit, ContextMenuStrip1.Font.GdiCharSet, ContextMenuStrip1.Font.GdiVerticalFont)
        'Font = New Font(Font.Name, 8.25F * 96.0F / CreateGraphics().DpiX, Font.Style, Font.Unit, Font.GdiCharSet, Font.GdiVerticalFont)
    End Sub
    Sub formatcolorcur()
        If crmd = 0 Then
            Me.ContextMenuStrip1.BackColor = Color.FromArgb(32, 32, 32)
            Me.ContextMenuStrip1.ForeColor = Color.White
            Me.ContextMenuStrip2.BackColor = Color.FromArgb(32, 32, 32)
            Me.ContextMenuStrip2.ForeColor = Color.White
            Me.BackColor = Color.FromArgb(32, 32, 32)
            Me.ForeColor = Color.White
            Me.Button1.BackColor = Color.Black
            Me.Button1.ForeColor = Color.White
            Me.Button1.FlatAppearance.BorderColor = Color.Black
            Me.Button1.FlatAppearance.MouseDownBackColor = Color.Gray
            Me.Button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(64, 64, 64)
            EnableDarkModeForWindow(Me.Handle, True)
        Else
            Me.Button1.BackColor = Color.White
            Me.Button1.ForeColor = Color.Black
            Me.Button1.FlatAppearance.BorderColor = Color.White
            Me.Button1.FlatAppearance.MouseDownBackColor = Color.LightGray
            Me.Button1.FlatAppearance.MouseOverBackColor = Color.Gainsboro
            Me.BackColor = Color.White
            Me.ForeColor = Color.Black
            Me.ContextMenuStrip1.BackColor = Color.White
            Me.ContextMenuStrip1.ForeColor = Color.Black
            Me.ContextMenuStrip2.BackColor = Color.White
            Me.ContextMenuStrip2.ForeColor = Color.Black
            EnableDarkModeForWindow(Me.Handle, False)
        End If
    End Sub


    Private Sub h30s_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles h30s.Click
        Timer2.Interval = 30000
        NotifyIcon1.Visible = True
        NotifyIcon1.ShowBalloonTip(7000, "一键关闭课件小工具", "一键关闭课件小工具当前已隐藏到系统托盘，双击托盘图标或在设定的时间（30秒）之后重新显示。", ToolTipIcon.Info)
        Me.Hide()
        Timer2.Enabled = True
    End Sub

    Private Sub h1m_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles h1m.Click
        Timer2.Interval = 60000
        NotifyIcon1.Visible = True
        NotifyIcon1.ShowBalloonTip(7000, "一键关闭课件小工具", "一键关闭课件小工具当前已隐藏到系统托盘，双击托盘图标或在设定的时间（1分钟）之后重新显示。", ToolTipIcon.Info)
        Me.Hide()
        Timer2.Enabled = True
    End Sub

    Private Sub h5m_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles h5m.Click
        Timer2.Interval = 300000
        NotifyIcon1.Visible = True
        NotifyIcon1.ShowBalloonTip(7000, "一键关闭课件小工具", "一键关闭课件小工具当前已隐藏到系统托盘，双击托盘图标或在设定的时间（5分钟）之后重新显示。", ToolTipIcon.Info)
        Me.Hide()
        Timer2.Enabled = True
    End Sub

    Private Sub h10m_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles h10m.Click
        Timer2.Interval = 600000
        NotifyIcon1.Visible = True
        NotifyIcon1.ShowBalloonTip(7000, "一键关闭课件小工具", "一键关闭课件小工具当前已隐藏到系统托盘，双击托盘图标或在设定的时间（10分钟）之后重新显示。", ToolTipIcon.Info)
        Me.Hide()
        Timer2.Enabled = True
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Me.Show()
        Timer2.Enabled = False
        NotifyIcon1.Visible = False
    End Sub

    Private Sub ext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ext.Click
        'If MessageBox.Show("确定要关闭时钟吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = MsgBoxResult.Yes Then
        'End
        'End If
        Form2.ShowDialog()
    End Sub

    Private Sub Me_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        'e.Cancel = True
        Select Case (e.CloseReason)
            '应用程序要求关闭窗口
            Case CloseReason.ApplicationExitCall
                e.Cancel = False '不拦截，响应操作
                '自身窗口上的关闭按钮
            Case CloseReason.FormOwnerClosing
                e.Cancel = True '拦截，不响应操作
                'MDI窗体关闭事件
            Case CloseReason.MdiFormClosing
                e.Cancel = True '拦截，不响应操作
                '不明原因的关闭
            Case CloseReason.None
                e.Cancel = False
                '任务管理器关闭进程
            Case CloseReason.TaskManagerClosing
                e.Cancel = True '拦截，不响应操作
                '用户通过UI关闭窗口或者通过Alt+F4关闭窗口
            Case CloseReason.UserClosing
                e.Cancel = True '拦截，不响应操作
                '操作系统准备关机()
            Case (CloseReason.WindowsShutDown)
                e.Cancel = False '不拦截，响应操作
        End Select

    End Sub

    'Private Sub Me_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles Me.FormClosed
    'Me.Dispose()
    'End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If CloseStateV = 0 Then
            If MessageBox.Show("确定要关闭课件吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                Button1.Enabled = False
                If Command().ToLower = "/lefttopbar" Or Command().ToLower = "/righttopbar" Or Command().ToLower = "/leftbottombar" Or Command().ToLower = "/rightbottombar" Or Command() = "/topbar" Or Command() = "/bottombar" Then
                    Button1.Text = "正在关闭"
                Else
                    'Button1.Text = "关" & vbCrLf & "闭" & vbCrLf & "课" & vbCrLf & "件"
                    Button1.Text = "正在关闭"
                End If
                Button1.Enabled = True
                MessageBox.Show("正在关闭课件，在按钮""正在关闭""文字变化之前，请不要再点击关闭按钮，以免重复关闭。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Try
                    For Each TargetNamea As String In TargetNames
                        Shell("taskkill.exe /im " & TargetNamea & ".exe", AppWinStyle.Hide)
                        Shell("taskkill.exe /im " & TargetNamea & "*", AppWinStyle.Hide)
                        Shell("taskkill.exe /f /im " & TargetNamea & ".exe", AppWinStyle.Hide)
                        Shell("taskkill.exe /f /im " & TargetNamea & "*", AppWinStyle.Hide, True)
                    Next

                    For Each TargetName As String In TargetNames
                        'Dim TargetName As String = "fmp" '存储进程名为文本型，注：进程名不加扩展名
                        Dim TargetKill() As Process = Process.GetProcessesByName(TargetName) '从进程名获取进程
                        Dim TargetPath As String '存储进程路径为文本型
                        If TargetKill.Length > 1 Then '判断进程名的数量，如果同名进程数量在2个以上，用For循环关闭进程。
                            For i = 0 To TargetKill.Length - 1
                                TargetPath = TargetKill(i).MainModule.FileName
                                TargetKill(i).Kill()
                            Next
                            'ElseIf TargetKill.Length = 0 Then '判断进程名的数量，没有发现进程直接弹窗。不需要的，可直接删掉该If子句
                            '   Exit Sub
                        ElseIf TargetKill.Length = 1 Then '判断进程名的数量，如果只有一个，就不用For循环
                            TargetKill(0).Kill()
                        End If
                        'Me.Dispose(1) '关闭自身进程
                    Next
                Catch ex As Exception
                End Try
                RenS = 0
                Timer3.Enabled = False
                CloseStateV = 0
                Form2.Button3.Enabled = True
                CurState = 0
                If Command().ToLower = "/lefttopbar" Or Command().ToLower = "/righttopbar" Or Command().ToLower = "/leftbottombar" Or Command().ToLower = "/rightbottombar" Or Command() = "/topbar" Or Command() = "/bottombar" Then
                    Button1.Text = "关闭课件"
                Else
                    'Button1.Text = "关" & vbCrLf & "闭" & vbCrLf & "课" & vbCrLf & "件"
                    Button1.Text = "关闭课件"
                End If
            End If
        Else
            CloseStateV = 0
            Form2.Button3.Enabled = True
            Timer3.Enabled = False
            CurState = 0
            RenS = 0
            If Command().ToLower = "/lefttopbar" Or Command().ToLower = "/righttopbar" Or Command().ToLower = "/leftbottombar" Or Command().ToLower = "/rightbottombar" Or Command() = "/topbar" Or Command() = "/bottombar" Then
                Button1.Text = "关闭课件"
            Else
                'Button1.Text = "关" & vbCrLf & "闭" & vbCrLf & "课" & vbCrLf & "件"
                Button1.Text = "关闭课件"
            End If
        End If
    End Sub

    Private Sub Timer3_Tick(sender As System.Object, e As System.EventArgs) Handles Timer3.Tick
        If RenS = 0 Then
            RenS = RenS - 1
            CloseStateV = 0
            CurState = 0
            RenS = -1

            ''' 旧版说明
            '目前支持关闭的程序名称：Word（WINWORD.EXE）Excel（EXCEL.EXE）PowerPoint（POWERPNT.EXE）WPS 文字（wps.exe）WPS 表格（et.exe）WPS 表格（et.exe）WPS 演示（wpp.exe）WPS PDF（wpspdf.exe）WPS Office（wpsoffice.exe）Edge（msedge.exe）Chrome（chrome.exe）FireFox（firefox.exe）希沃白板（EasiNote.exe）希沃视频展台（EasiCamera.exe）微信（Wechat.exe）外研电子课本（db.exe）
            '''

            '支持关闭程序关键字列表
            'WINWORD,EXCEL,POWERPNT,
            'wps,et,wpp,
            'wpspdf,wpsoffice,msedge,
            'chrome,firefox,EasiNote,
            'EasiCamera,Wechat,db,
            'Cbox,qyplayer,QQLive,
            'kugou,kuwomusic,wpspic,
            'iexplore,PotPlayer,PotPlayerMini,
            'PhotosApp,PhotosService,Microsoft.Photos,
            'Microsoft.Media.Player,
            'Groove,WindowsCamera,SoundRec,
            'CalculatorApp,calc,notepad,
            'rundll32,dllhost,mspaint,
            'wmplayer,Video.UI,SnippingTool,
            'PotPlayerMini64,360chrome,360se,
            'winrar,winzip,7z,
            '7zFM,bandzip,theworld,
            'liebao,qingniao,Twinkstar,
            'UCBrowser,UCService,2345Explorer,
            'quark,iexplore,QQBrowser,
            'Chromium,SeewoBrowser,360chromex,
            '360aibrowser,QQPlayer,baofeng,
            'SLBrowser,SLB,QQ,
            'StormPlayer,SogouExplorer,NimoNavigator,
            'CamShow,ScreenBoard,Nimo,
            'HiteCamera,HitePai,nanazip,
            'haozip,360zip,MicrosoftEdge
            Button1.Enabled = False
            If Command().ToLower = "/lefttopbar" Or Command().ToLower = "/righttopbar" Or Command().ToLower = "/leftbottombar" Or Command().ToLower = "/rightbottombar" Or Command() = "/topbar" Or Command() = "/bottombar" Then
                Button1.Text = "正在关闭"
            Else
                'Button1.Text = "关" & vbCrLf & "闭" & vbCrLf & "课" & vbCrLf & "件"
                Button1.Text = "正在关闭"
            End If
            Button1.Enabled = True
            MessageBox.Show("正在关闭课件，在按钮""正在关闭""文字变化之前，请不要再点击关闭按钮，以免重复关闭。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Try
                For Each TargetNamea As String In TargetNames
                    Shell("taskkill.exe /im " & TargetNamea & ".exe", AppWinStyle.Hide)
                    Shell("taskkill.exe /im " & TargetNamea & "*", AppWinStyle.Hide)
                    Shell("taskkill.exe /f /im " & TargetNamea & ".exe", AppWinStyle.Hide)
                    Shell("taskkill.exe /f /im " & TargetNamea & "*", AppWinStyle.Hide, True)
                Next

                For Each TargetName As String In TargetNames
                    'Dim TargetName As String = "fmp" '存储进程名为文本型，注：进程名不加扩展名
                    Dim TargetKill() As Process = Process.GetProcessesByName(TargetName) '从进程名获取进程
                    Dim TargetPath As String '存储进程路径为文本型
                    If TargetKill.Length > 1 Then '判断进程名的数量，如果同名进程数量在2个以上，用For循环关闭进程。
                        For i = 0 To TargetKill.Length - 1
                            TargetPath = TargetKill(i).MainModule.FileName
                            TargetKill(i).Kill()
                        Next
                        'ElseIf TargetKill.Length = 0 Then '判断进程名的数量，没有发现进程直接弹窗。不需要的，可直接删掉该If子句
                        '   Exit Sub
                    ElseIf TargetKill.Length = 1 Then '判断进程名的数量，如果只有一个，就不用For循环
                        TargetKill(0).Kill()
                    End If
                    'Me.Dispose(1) '关闭自身进程
                Next
            Catch ex As Exception
            End Try
            If Command().ToLower = "/lefttopbar" Or Command().ToLower = "/righttopbar" Or Command().ToLower = "/leftbottombar" Or Command().ToLower = "/rightbottombar" Or Command() = "/topbar" Or Command() = "/bottombar" Then
                Button1.Text = "关闭课件"
            Else
                'Button1.Text = "关" & vbCrLf & "闭" & vbCrLf & "课" & vbCrLf & "件"
                Button1.Text = "关闭课件"
            End If
            Form2.Button3.Enabled = True
            RenS = 0
            Timer3.Enabled = False
        Else
            If RenS >= 0 Then
                RenS = RenS - 1
                If Command().ToLower = "/lefttopbar" Or Command().ToLower = "/righttopbar" Or Command().ToLower = "/leftbottombar" Or Command().ToLower = "/rightbottombar" Or Command() = "/topbar" Or Command() = "/bottombar" Then
                    Button1.Text = "取消 " & RenS & "s"
                Else
                    Button1.Text = "取消" & RenS & "s"
                    'Button1.Text = "取" & vbCrLf & "消" & vbCrLf & RenS & vbCrLf & "s"
                End If

            End If
        End If
    End Sub

    Private Sub fifteenclose_Click(sender As System.Object, e As System.EventArgs) Handles fifteenclose.Click
        If CloseStateV = 0 Then
            CloseStateV = 1
            Form2.Button3.Enabled = False
            Timer3.Enabled = True
            CurState = 1
            RenS = 15
            If Command().ToLower = "/lefttopbar" Or Command().ToLower = "/righttopbar" Or Command().ToLower = "/leftbottombar" Or Command().ToLower = "/rightbottombar" Or Command() = "/topbar" Or Command() = "/bottombar" Then
                Button1.Text = "取消 15s"
            Else
                'Button1.Text = "取" & vbCrLf & "消" & vbCrLf & "15" & vbCrLf & "s" 
                Button1.Text = "取消15s"
            End If
        Else
            CloseStateV = 0
            Form2.Button3.Enabled = True
            Timer3.Enabled = False
            CurState = 0
            RenS = 0
            If Command().ToLower = "/lefttopbar" Or Command().ToLower = "/righttopbar" Or Command().ToLower = "/leftbottombar" Or Command().ToLower = "/rightbottombar" Or Command() = "/topbar" Or Command() = "/bottombar" Then
                Button1.Text = "关闭课件"
            Else
                'Button1.Text = "关" & vbCrLf & "闭" & vbCrLf & "课" & vbCrLf & "件"
                Button1.Text = "关闭课件"
            End If
        End If
    End Sub

    Private Sub thirtyclose_Click(sender As System.Object, e As System.EventArgs) Handles thirtyclose.Click
        If CloseStateV = 0 Then
            CloseStateV = 1
            Form2.Button3.Enabled = False
            Timer3.Enabled = True
            CurState = 1
            RenS = 30
            If Command().ToLower = "/lefttopbar" Or Command().ToLower = "/righttopbar" Or Command().ToLower = "/leftbottombar" Or Command().ToLower = "/rightbottombar" Or Command() = "/topbar" Or Command() = "/bottombar" Then
                Button1.Text = "取消 30s"
            Else
                'Button1.Text = "取" & vbCrLf & "消" & vbCrLf & "15" & vbCrLf & "s"
                Button1.Text = "取消30s"
            End If
        Else
            CloseStateV = 0
            Form2.Button3.Enabled = True
            Timer3.Enabled = False
            CurState = 0
            RenS = 0
            If Command().ToLower = "/lefttopbar" Or Command().ToLower = "/righttopbar" Or Command().ToLower = "/leftbottombar" Or Command().ToLower = "/rightbottombar" Or Command() = "/topbar" Or Command() = "/bottombar" Then
                Button1.Text = "关闭课件"
            Else
                'Button1.Text = "关" & vbCrLf & "闭" & vbCrLf & "课" & vbCrLf & "件"
                Button1.Text = "关闭课件"
            End If
        End If
    End Sub

    Private Sub sixtyclose_Click(sender As System.Object, e As System.EventArgs) Handles sixtyclose.Click
        If CloseStateV = 0 Then
            CloseStateV = 1
            Form2.Button3.Enabled = False
            Timer3.Enabled = True
            CurState = 1
            RenS = 60
            If Command().ToLower = "/lefttopbar" Or Command().ToLower = "/righttopbar" Or Command().ToLower = "/leftbottombar" Or Command().ToLower = "/rightbottombar" Or Command() = "/topbar" Or Command() = "/bottombar" Then
                Button1.Text = "取消 60s"
            Else
                'Button1.Text = "取" & vbCrLf & "消" & vbCrLf & "15" & vbCrLf & "s" 
                Button1.Text = "取消60s"
            End If
        Else
            CloseStateV = 0
            Form2.Button3.Enabled = True
            Timer3.Enabled = False
            CurState = 0
            RenS = 0
            If Command().ToLower = "/lefttopbar" Or Command().ToLower = "/righttopbar" Or Command().ToLower = "/leftbottombar" Or Command().ToLower = "/rightbottombar" Or Command() = "/topbar" Or Command() = "/bottombar" Then
                Button1.Text = "关闭课件"
            Else
                'Button1.Text = "关" & vbCrLf & "闭" & vbCrLf & "课" & vbCrLf & "件"
                Button1.Text = "关闭课件"
            End If
        End If
    End Sub
    Private Sub SystemEvents_UserPreferenceChanged(ByVal sender As Object, ByVal e As UserPreferenceChangedEventArgs)
        If e.Category = UserPreferenceCategory.General Then
            If appcolor = 0 Then
                'Get System Color
                Dim regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", True)
                Dim sysacr As Integer
                If (Not regkey Is Nothing) Then
                    'If (If((regkey.GetValue("") Is Nothing), Nothing, regkey.GetValue("").ToString) <> "AppsUseLightTheme") Then
                    'End If
                    sysacr = regkey.GetValue("AppsUseLightTheme", -1)
                    If sysacr = 0 Then
                        crmd = 0
                    ElseIf sysacr = 1 Then
                        crmd = 1
                    Else
                        crmd = 1
                    End If
                Else
                    crmd = 1
                End If
                regkey.Close()
                Call formatcolorcur()
                Call Form2.formatcolorcurset()
                Call GPLForm.formatcolorcursetmsg()
            End If
        End If
    End Sub

    Private Sub fiveclose_Click(sender As System.Object, e As System.EventArgs) Handles fiveclose.Click
        If CloseStateV = 0 Then
            CloseStateV = 1
            Form2.Button3.Enabled = False
            Timer3.Enabled = True
            CurState = 1
            RenS = 5
            If Command().ToLower = "/lefttopbar" Or Command().ToLower = "/righttopbar" Or Command().ToLower = "/leftbottombar" Or Command().ToLower = "/rightbottombar" Or Command() = "/topbar" Or Command() = "/bottombar" Then
                Button1.Text = "取消 5s"
            Else
                'Button1.Text = "取" & vbCrLf & "消" & vbCrLf & "15" & vbCrLf & "s" 
                Button1.Text = "取消5s"
            End If
        Else
            CloseStateV = 0
            Form2.Button3.Enabled = True
            Timer3.Enabled = False
            CurState = 0
            RenS = 0
            If Command().ToLower = "/lefttopbar" Or Command().ToLower = "/righttopbar" Or Command().ToLower = "/leftbottombar" Or Command().ToLower = "/rightbottombar" Or Command() = "/topbar" Or Command() = "/bottombar" Then
                Button1.Text = "关闭课件"
            Else
                'Button1.Text = "关" & vbCrLf & "闭" & vbCrLf & "课" & vbCrLf & "件"
                Button1.Text = "关闭课件"
            End If
        End If
    End Sub
    Private Sub NotifyIcon1_MouseDoubleClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Me.Show()
        Timer2.Enabled = False
        NotifyIcon1.Visible = False
    End Sub

    Private Sub shtbar_Click(sender As System.Object, e As System.EventArgs) Handles shtbar.Click
        Me.Show()
        Timer2.Enabled = False
        NotifyIcon1.Visible = False
    End Sub
End Class

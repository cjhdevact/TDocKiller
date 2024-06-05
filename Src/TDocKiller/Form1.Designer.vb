<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.fiveclose = New System.Windows.Forms.ToolStripMenuItem()
        Me.fifteenclose = New System.Windows.Forms.ToolStripMenuItem()
        Me.thirtyclose = New System.Windows.Forms.ToolStripMenuItem()
        Me.sixtyclose = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.h30s = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.h1m = New System.Windows.Forms.ToolStripMenuItem()
        Me.h5m = New System.Windows.Forms.ToolStripMenuItem()
        Me.h10m = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ext = New System.Windows.Forms.ToolStripMenuItem()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Timer3 = New System.Windows.Forms.Timer(Me.components)
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.shtbar = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.ContextMenuStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.BackColor = System.Drawing.Color.White
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.fiveclose, Me.fifteenclose, Me.thirtyclose, Me.sixtyclose, Me.ToolStripSeparator2, Me.h30s, Me.ToolStripSeparator1, Me.h1m, Me.h5m, Me.h10m, Me.ToolStripSeparator3, Me.ext})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(144, 220)
        '
        'fiveclose
        '
        Me.fiveclose.Name = "fiveclose"
        Me.fiveclose.Size = New System.Drawing.Size(143, 22)
        Me.fiveclose.Text = "5秒后关闭"
        '
        'fifteenclose
        '
        Me.fifteenclose.Name = "fifteenclose"
        Me.fifteenclose.Size = New System.Drawing.Size(143, 22)
        Me.fifteenclose.Text = "15秒后关闭"
        '
        'thirtyclose
        '
        Me.thirtyclose.Name = "thirtyclose"
        Me.thirtyclose.Size = New System.Drawing.Size(143, 22)
        Me.thirtyclose.Text = "30秒后关闭"
        '
        'sixtyclose
        '
        Me.sixtyclose.Name = "sixtyclose"
        Me.sixtyclose.Size = New System.Drawing.Size(143, 22)
        Me.sixtyclose.Text = "1分钟后关闭"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(140, 6)
        '
        'h30s
        '
        Me.h30s.Name = "h30s"
        Me.h30s.Size = New System.Drawing.Size(143, 22)
        Me.h30s.Text = "隐藏30秒"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(140, 6)
        '
        'h1m
        '
        Me.h1m.Name = "h1m"
        Me.h1m.Size = New System.Drawing.Size(143, 22)
        Me.h1m.Text = "隐藏1分钟"
        '
        'h5m
        '
        Me.h5m.Name = "h5m"
        Me.h5m.Size = New System.Drawing.Size(143, 22)
        Me.h5m.Text = "隐藏5分钟"
        '
        'h10m
        '
        Me.h10m.Name = "h10m"
        Me.h10m.Size = New System.Drawing.Size(143, 22)
        Me.h10m.Text = "隐藏10分钟"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(140, 6)
        '
        'ext
        '
        Me.ext.Name = "ext"
        Me.ext.Size = New System.Drawing.Size(143, 22)
        Me.ext.Text = "更多..."
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'Timer2
        '
        '
        'Button1
        '
        Me.Button1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(0, 0)
        Me.Button1.Margin = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(162, 31)
        Me.Button1.TabIndex = 0
        Me.Button1.TabStop = False
        Me.Button1.Text = "关闭课件"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Timer3
        '
        Me.Timer3.Interval = 1000
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.ContextMenuStrip = Me.ContextMenuStrip2
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "一键关闭课件小工具"
        '
        'ContextMenuStrip2
        '
        Me.ContextMenuStrip2.BackColor = System.Drawing.Color.White
        Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.shtbar})
        Me.ContextMenuStrip2.Name = "ContextMenuStrip2"
        Me.ContextMenuStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ContextMenuStrip2.Size = New System.Drawing.Size(116, 26)
        '
        'shtbar
        '
        Me.shtbar.Name = "shtbar"
        Me.shtbar.Size = New System.Drawing.Size(115, 22)
        Me.shtbar.Text = "显示(&S)"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.Black
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(162, 31)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ForeColor = System.Drawing.Color.Black
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.Opacity = 0.75R
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.TransparencyKey = System.Drawing.Color.FromArgb(CType(CType(185, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ContextMenuStrip2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents h30s As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents h1m As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents h5m As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents h10m As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ext As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Timer3 As System.Windows.Forms.Timer
    Friend WithEvents fifteenclose As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents thirtyclose As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sixtyclose As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents fiveclose As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents ContextMenuStrip2 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents shtbar As System.Windows.Forms.ToolStripMenuItem

End Class

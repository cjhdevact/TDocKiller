﻿'------------------------------------------------------------------------------
' <auto-generated>
'     此代码由工具生成。
'     运行时版本:4.0.30319.42000
'
'     对此文件的更改可能会导致不正确的行为，并且如果
'     重新生成代码，这些更改将会丢失。
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    '此类是由 StronglyTypedResourceBuilder
    '类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    '若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    '(以 /str 作为命令选项)，或重新生成 VS 项目。
    '''<summary>
    '''  一个强类型的资源类，用于查找本地化的字符串等。
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>  _
    Friend Module Resources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  返回此类使用的缓存的 ResourceManager 实例。
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("TDocKiller.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  使用此强类型资源类，为所有资源查找
        '''  重写当前线程的 CurrentUICulture 属性。
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  查找类似 一、一键关闭课件支持关闭的程序
        '''  目前支持关闭包括以下关键字名称的程序：
        '''  1.办公软件
        '''  (1)Microsoft Office
        '''    WINWORD,EXCEL,POWERPNT
        '''  (2)WPS
        '''    wps,et,wpp,wpspdf,wpsoffice,wpspic
        '''  2.通讯工具（微信，QQ，Tim，钉钉）
        '''    Wechat,Weixin,QQ,Tim,DingTalk
        '''  3.教学软件
        '''  (1)希沃
        '''    EasiNote,EasiCamera
        '''  (2)嘉宏高拍仪
        '''    NimoNavigator,CamShow,ScreenBoard,Nimo
        '''  (3)鸿合
        '''    HiteCamera,HitePai
        '''  (4)联想
        '''    Lenovo.Smart.BoardTools,Lenovo.Smart.SubjectTools,SmartClass,SmartClassPlayer,
        '''    SmartClassService,SmartClassShell,SmartRecorder,BlackboardWriting,Deskto [字符串的其余部分被截断]&quot;; 的本地化字符串。
        '''</summary>
        Friend ReadOnly Property DocText() As String
            Get
                Return ResourceManager.GetString("DocText", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  查找类似                     GNU GENERAL PUBLIC LICENSE
        '''                       Version 3, 29 June 2007
        '''
        ''' Copyright (C) 2007 Free Software Foundation, Inc. &lt;http://fsf.org/&gt;
        ''' Everyone is permitted to copy and distribute verbatim copies
        ''' of this license document, but changing it is not allowed.
        '''
        '''                            Preamble
        '''
        '''  The GNU General Public License is a free, copyleft license for
        '''software and other kinds of works.
        '''
        '''  The licenses for most software and other practical works are designed
        '''to [字符串的其余部分被截断]&quot;; 的本地化字符串。
        '''</summary>
        Friend ReadOnly Property GPL3() As String
            Get
                Return ResourceManager.GetString("GPL3", resourceCulture)
            End Get
        End Property
    End Module
End Namespace

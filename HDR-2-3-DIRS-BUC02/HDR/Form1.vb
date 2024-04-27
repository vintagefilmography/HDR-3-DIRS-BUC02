Imports TIS.Imaging

Public Class Form1
    Dim VideoHasStarted As Boolean
    Dim StartToggle As Integer = 0
    Dim ZoomOutValue As Decimal = 1.2
    Dim TriggerToggle As Integer = 0
    Dim ImgSaveToggle As Integer = 0
    Dim BitToggle As Integer = 0
    Dim TiffToggle As Integer = 0
    Dim impath0 As String
    Dim impathLo As String
    Dim impathHi As String
    Dim imFullpathLo As String = "C:\"
    Dim imFullpathHi As String = "C:\"
    Dim ImFullpath0 As String = "C:\"
    Dim IncrementLow As Integer = 0
    Dim ImageCount As Integer = 0
    Dim SequenceCount As Integer = 0
    Dim ExposureProperty As VCDRangeProperty
    Dim ExposureAuto As VCDSwitchProperty
    Dim ImageOdd As Integer = 0
    Dim ImageEven As Integer = 0
    Dim ImageReg As Integer = 0
    Dim ImageSaveCount As Integer = 0
    Dim Exp2Toggle As Integer = 0


    Private _imageSink As TIS.Imaging.FrameSnapSink
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error GoTo err_Form_Load
        If Not IcImagingControl1.DeviceValid Then
            IcImagingControl1.ShowDeviceSettingsDialog()

            If Not IcImagingControl1.DeviceValid Then
                MsgBox("No device was selected.")

                Exit Sub
            End If
        End If
        IcImagingControl1.LiveDisplay = True
        IcImagingControl1.LiveCaptureLastImage = False
        IcImagingControl1.LiveCaptureContinuous = True


        IcImagingControl1.Width = IcImagingControl1.ImageWidth
        IcImagingControl1.Height = IcImagingControl1.ImageHeight
        'IcImagingControl1.MemoryCurrentGrabberColorformat = ICImagingControlColorformats.ICRGB64
        IcImagingControl1.MemoryCurrentGrabberColorformat = ICImagingControlColorformats.ICRGB32

        'use this for tiff images
        '_imageSink = New TIS.Imaging.FrameSnapSink(TIS.Imaging.MediaSubtypes.RGB64)
        'IcImagingControl1.Sink = _imageSink
        'IcImagingControl1.ImageAvailableExecutionMode = 1

        Exit Sub

err_Form_Load:
        MsgBox(Err.Description)
    End Sub
    Private Sub ShowBuffer(buffer As IFrameQueueBuffer)
        Try
            IcImagingControl1.DisplayImageBuffer(buffer)
        Catch
            MessageBox.Show("snap image failed, timeout occurred.")
        End Try
    End Sub






    Private Sub IcImagingControl1_Load(sender As Object, e As EventArgs) Handles IcImagingControl1.Load

    End Sub

    Private Sub Start_Click(sender As Object, e As EventArgs) Handles Start.Click
        If StartToggle = 0 Then
            Start.BackColor = Color.Red
            StartToggle = 1
            IcImagingControl1.LiveStart()
            VideoHasStarted = True
        Else
            Start.BackColor = Color.White
            StartToggle = 0
            IcImagingControl1.LiveStop()
            VideoHasStarted = False
        End If
    End Sub

    Private Sub zoomout_click(sender As Object, e As EventArgs) Handles ZoomOut.Click
        IcImagingControl1.LiveDisplayDefault = 0
        IcImagingControl1.LiveDisplayZoomFactor = IcImagingControl1.LiveDisplayZoomFactor / ZoomOutValue
    End Sub

    Private Sub zoomin_click(sender As Object, e As EventArgs) Handles ZoomIn.Click
        IcImagingControl1.LiveDisplayDefault = 0
        IcImagingControl1.LiveDisplayZoomFactor = IcImagingControl1.LiveDisplayZoomFactor * ZoomOutValue
    End Sub

    Private Sub settings_click(sender As Object, e As EventArgs) Handles Settings.Click
        IcImagingControl1.ShowPropertyDialog()
    End Sub

    Private Sub trigger_click(sender As Object, e As EventArgs) Handles Trigger.Click
        If TriggerToggle = 0 Then
            Trigger.BackColor = Color.Red
            TriggerToggle = 1
            IcImagingControl1.DeviceTrigger = True
        Else
            Trigger.BackColor = Color.White
            TriggerToggle = 0
            IcImagingControl1.DeviceTrigger = False
        End If
    End Sub

    Private Sub imgsave_click(sender As Object, e As EventArgs) Handles ImgSave.Click
        If ImgSaveToggle = 0 Then
            ImgSave.BackColor = Color.Red
            ImgSaveToggle = 1
        Else
            ImgSave.BackColor = Color.White
            ImgSaveToggle = 0
        End If

    End Sub

    Private Sub PathLo_click(sender As Object, e As EventArgs) Handles PathLo.Click
        If impathLo = "" Then
            Dim dialog As New FolderBrowserDialog()
            dialog.RootFolder = Environment.SpecialFolder.Desktop
            dialog.SelectedPath = "c:\"
            dialog.Description = "select application configeration files path"
            If dialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                impathLo = dialog.SelectedPath
            End If
            'my.computer.filesystem.writealltext(impath & "apppath.txt", impath, false)
            Debug.Print(impathLo)
        End If
    End Sub

    Private Sub PathHi_click(sender As Object, e As EventArgs) Handles PathHi.Click
        If impathHi = "" Then
            Dim dialog As New FolderBrowserDialog()
            dialog.RootFolder = Environment.SpecialFolder.Desktop
            dialog.SelectedPath = "c:\"
            dialog.Description = "select application configeration files path"
            If dialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                impathHi = dialog.SelectedPath
            End If
            'my.computer.filesystem.writealltext(impath & "apppath.txt", impath, false)
            Debug.Print(impathHi)
        End If
    End Sub

    Private Sub inclow_click(sender As Object, e As EventArgs) Handles IncLow.Click
        MessageBox.Show("Exposure increment=" + Str(IncrementLow))
    End Sub

    Private Sub icimagingcontrol1_imageavailable(sender As Object, e As ICImagingControl.ImageAvailableEventArgs) Handles IcImagingControl1.ImageAvailable

        On Error GoTo err_imageavailable_handler

        Dim CurrentBuffer As ImageBuffer
        Dim DisplayBuffer As ImageBuffer

        'snap(Capture) an image To the memory
        'Dim image As TIS.Imaging.IFrameQueueBuffer
        'IcImagingControl1.LiveStop()
        'IcImagingControl1.Sink = _imageSink
        'Try
        'image = _imageSink.SnapSingle(TimeSpan.FromSeconds(1))
        'Catch
        'MessageBox.Show("snap image failed, timeout occurred.")
        'End Try
        'image.SaveAsTiff("d:\hawkeye\hdr\grab3.tiff")
        'Dim buffer_type As Integer
        'buffer_type = IcImagingControl1.ImageActiveBuffer.BitsPerPixel
        'buffer_type = IcImagingControl1.Sink.SinkType
        CurrentBuffer = IcImagingControl1.ImageActiveBuffer
        'IcImagingControl1.DisplayImageBuffer(CurrentBuffer)
        'save images. odd in path1 and even in path2
        If ImgSaveToggle = 1 Then

            ExposureProperty = IcImagingControl1.VCDPropertyItems.FindInterface(VCDGUIDs.VCDID_Exposure, VCDGUIDs.VCDElement_Value, VCDGUIDs.VCDInterface_Range)
            ExposureAuto = IcImagingControl1.VCDPropertyItems.FindInterface(VCDGUIDs.VCDID_Exposure, VCDGUIDs.VCDElement_Auto, VCDGUIDs.VCDInterface_Switch)
            SequenceCount = SequenceCount + 1
            ImageCount = ImageCount + 1
            ImageSaveCount = ImageSaveCount + 1


            ' ========    TWO EXPOSURES  ===========
            If Exp2Toggle = 1 Then
                If SequenceCount = 3 Then
                    SequenceCount = 1
                End If
                If ImageCount > 0 Then
                    If SequenceCount = 1 Then
                        ExposureProperty.Value = ExposureProperty.Value - IncrementLow
                        'ExposureAuto.Switch = True
                        ImageOdd = ImageOdd + 1
                        impathHi = impathHi.Trim(vbNullChar)
                        If TiffToggle = 1 Then
                            imFullpathHi = impathLo + "\" + Str(ImageOdd) + ".tiff"
                        Else
                            imFullpathHi = impathHi + "\" + Str(ImageOdd) + ".jpg"
                        End If
                        imFullpathHi = imFullpathHi.Replace(" ", "")
                        If TiffToggle = 1 Then
                            CurrentBuffer.SaveAsTiff(imFullpathHi)
                        Else
                            CurrentBuffer.SaveAsJpeg(imFullpathHi, 100)
                        End If
                    End If

                    If SequenceCount = 2 Then
                        ExposureProperty.Value = ExposureProperty.Value + IncrementLow
                        'ExposureAuto.Switch = True
                        ImageEven = ImageEven + 1
                        impathLo = impathLo.Trim(vbNullChar)
                        If TiffToggle = 1 Then
                            imFullpathLo = impathLo + "\" + Str(ImageEven) + ".tiff"
                        Else
                            imFullpathLo = impathLo + "\" + Str(ImageEven) + ".jpg"
                        End If
                        imFullpathLo = imFullpathLo.Replace(" ", "")
                        If TiffToggle = 1 Then
                            CurrentBuffer.SaveAsTiff(imFullpathLo)
                        Else
                            CurrentBuffer.SaveAsJpeg(imFullpathLo, 100)
                        End If
                    End If
                End If
            End If

            '=========== END OF TWO EXPOSURES ============

            '========    THREE EXPOSURES  ================
            If Exp2Toggle = 0 Then
                If SequenceCount = 4 Then
                    SequenceCount = 1
                End If
                If ImageCount > 0 Then
                    If SequenceCount = 1 Then
                        'ExposureAuto.Switch = False
                        ExposureProperty.Value = ExposureProperty.Value + (IncrementLow / 10)
                        ImageReg = ImageReg + 1
                        impath0 = impath0.Trim(vbNullChar)
                        If TiffToggle = 1 Then
                            ImFullpath0 = impath0 + "\" + Str(ImageReg) + ".tiff"
                        Else
                            ImFullpath0 = impath0 + "\" + Str(ImageReg) + ".jpg"
                        End If
                        ImFullpath0 = ImFullpath0.Replace(" ", "")
                        If TiffToggle = 1 Then
                            CurrentBuffer.SaveAsTiff(ImFullpath0)
                        Else
                            CurrentBuffer.SaveAsJpeg(ImFullpath0, 100)
                        End If
                    End If

                    If SequenceCount = 2 Then
                        ExposureProperty.Value = ExposureProperty.Value - (2 * IncrementLow / 10)
                        'ExposureAuto.Switch = True
                        ImageOdd = ImageOdd + 1
                        impathHi = impathHi.Trim(vbNullChar)
                        If TiffToggle = 1 Then
                            imFullpathHi = impathHi + "\" + Str(ImageOdd) + ".tiff"
                        Else
                            imFullpathHi = impathHi + "\" + Str(ImageOdd) + ".jpg"
                        End If
                        imFullpathHi = imFullpathHi.Replace(" ", "")
                        If TiffToggle = 1 Then
                            CurrentBuffer.SaveAsTiff(imFullpathHi)
                        Else
                            CurrentBuffer.SaveAsJpeg(imFullpathHi, 100)
                        End If
                    End If


                    If SequenceCount = 3 Then
                        ExposureProperty.Value = ExposureProperty.Value + IncrementLow / 10
                        'ExposureAuto.Switch = True
                        ImageEven = ImageEven + 1
                        impathLo = impathLo.Trim(vbNullChar)
                        If TiffToggle = 1 Then
                            imFullpathLo = impathLo + "\" + Str(ImageEven) + ".tiff"
                        Else
                            imFullpathLo = impathLo + "\" + Str(ImageEven) + ".jpg"
                        End If
                        imFullpathLo = imFullpathLo.Replace(" ", "")
                        If TiffToggle = 1 Then
                            CurrentBuffer.SaveAsTiff(imFullpathLo)
                        Else
                            CurrentBuffer.SaveAsJpeg(imFullpathLo, 100)
                        End If
                    End If
                End If
                ' =========== END OF THREE EXPOSURES ===============

            End If
        End If
        Debug.Print("image count=" + CStr(ImageCount))
        Debug.Print("imageodd=" + Str(ImageOdd))
        Debug.Print("imageeven=" + Str(ImageEven))
            Debug.Print(imFullpathLo)
            Debug.Print(imFullpathHi)
            'catch
            'messagebox.show("snap image failed, timeout occurred.")
            'end try
err_imageavailable_handler:
        Debug.Print(Err.Description)
    End Sub

    Private Sub SaveConf_Click(sender As Object, e As EventArgs) Handles SaveConf.Click
        IcImagingControl1.SaveDeviceStateToFile("device_state.txt")
    End Sub

    Private Sub LoadConf_Click(sender As Object, e As EventArgs) Handles LoadConf.Click
        IcImagingControl1.LoadDeviceStateFromFile("device_state.txt", True)
    End Sub

    Private Sub Bit64_Click(sender As Object, e As EventArgs) Handles Bit64.Click
        If BitToggle = 0 Then
            Bit64.BackColor = Color.Red
            BitToggle = 1
            IcImagingControl1.MemoryCurrentGrabberColorformat = ICImagingControlColorformats.ICRGB64
        Else
            Bit64.BackColor = Color.White
            BitToggle = 0
            IcImagingControl1.MemoryCurrentGrabberColorformat = ICImagingControlColorformats.ICRGB24
        End If
    End Sub

    Private Sub SaveTiff_Click(sender As Object, e As EventArgs) Handles SaveTiff.Click
        If TiffToggle = 0 Then
            SaveTiff.BackColor = Color.Red
            TiffToggle = 1
        Else
            SaveTiff.BackColor = Color.White
            TiffToggle = 0
        End If
    End Sub
    Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown1.ValueChanged
        'NumericUpDown valu will show in TextBox
        IncrementLow = NumericUpDown1.Value
    End Sub
    Private Sub NumericUpDown2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown2.ValueChanged
        'NumericUpDown valu will show in TextBox
        ImageSaveCount = NumericUpDown2.Value
    End Sub
    Private Sub showimgnum_click(sender As Object, e As EventArgs) Handles ShowImgNum.Click
        MessageBox.Show("Img start number =" + Str(ImageSaveCount))
    End Sub


    Private Sub Path0_click(sender As Object, e As EventArgs) Handles Path0.Click
        If impath0 = "" Then
            Dim dialog As New FolderBrowserDialog()
            dialog.RootFolder = Environment.SpecialFolder.Desktop
            dialog.SelectedPath = "c:\"
            dialog.Description = "select application configeration files path"
            If dialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                impath0 = dialog.SelectedPath
            End If
            'my.computer.filesystem.writealltext(impath & "apppath.txt", impath, false)
            Debug.Print(impath0)
        End If
    End Sub

    Private Sub Exp2_Click(sender As Object, e As EventArgs) Handles Exp2.Click
        If Exp2Toggle = 0 Then
            Exp2.BackColor = Color.Red
            Exp2Toggle = 1
        Else
            Exp2.BackColor = Color.White
            Exp2Toggle = 0
        End If
    End Sub
End Class

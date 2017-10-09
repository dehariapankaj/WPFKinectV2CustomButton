using Microsoft.Kinect.Input;
using Microsoft.Kinect.Toolkit.Input;
using Microsoft.Kinect.Wpf.Controls;
using System;
using System.Windows;

namespace WpfKinectV2CustomButton
{
    /// <summary>
    /// Kinect v2 custom button to support the hand enter and hand leave events
    /// </summary>
    public class KinectV2CustomButtonController : IKinectPressableController
    {
        private PressableModel _inputModel;
        private KinectRegion _kinectRegion;
        private KinectV2CustomButton _interactiveElement;
        private bool _disposedValue;
        KinectCoreWindow kinectCoreWindow = KinectCoreWindow.GetForCurrentThread();
        event EventHandler _handPointerEnter;
        event EventHandler _handPointerLeave;
        private bool _isHandPointerEntered;

        public KinectV2CustomButtonController(IInputModel inputModel, KinectRegion kinectRegion, EventHandler HandPointerEnter, EventHandler HandPointerLeave)
        {
            _inputModel = inputModel as PressableModel;
            _handPointerEnter = HandPointerEnter;
            _handPointerLeave = HandPointerLeave;
            _kinectRegion = kinectRegion;
            _interactiveElement = _inputModel.Element as KinectV2CustomButton;
            kinectCoreWindow.PointerMoved += kinectCoreWindow_PointerMoved;
        }

        private void kinectCoreWindow_PointerMoved(object sender, KinectPointerEventArgs args)
        {
            KinectPointerPoint kinectPointerPoint = args.CurrentPoint;
            if (kinectPointerPoint.Properties.IsEngaged)
            {
                // PointerPoints come with X/Y positions from 0.0 to 1.0
                // To calculate the corresponding point in pixels, we multiple by width/height of KinectRegion
                Point pointRelativeToKinectRegion = new Point(
                    kinectPointerPoint.Position.X * _kinectRegion.ActualWidth,
                    kinectPointerPoint.Position.Y * _kinectRegion.ActualHeight);

                Point relativeToElement = _kinectRegion.TranslatePoint(pointRelativeToKinectRegion, _interactiveElement);
                bool insideElement = (relativeToElement.X >= 0 && relativeToElement.X < _interactiveElement.ActualWidth
                    && relativeToElement.Y >= 0 && relativeToElement.Y < _interactiveElement.ActualHeight);
                if (insideElement)
                {
                    if (!_isHandPointerEntered)
                    {
                        _isHandPointerEntered = true;
                        _handPointerEnter?.Invoke(_interactiveElement, null);
                    }
                }
                else
                {
                    if (_isHandPointerEntered)
                    {
                        _isHandPointerEntered = false;
                        _handPointerLeave?.Invoke(_interactiveElement, null);
                    }
                }
            }
        }

        PressableModel IKinectPressableController.PressableInputModel
        {
            get { return _inputModel; }
        }
        FrameworkElement IKinectController.Element
        {
            get { return _inputModel.Element as FrameworkElement; }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                _kinectRegion = null;
                _inputModel = null;
                _interactiveElement = null;
                kinectCoreWindow.PointerMoved -= kinectCoreWindow_PointerMoved;
                _handPointerEnter = null;
                _handPointerLeave = null;
                kinectCoreWindow = null;
                _disposedValue = true;
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
        }
    }
}

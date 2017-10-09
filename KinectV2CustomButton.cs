using Microsoft.Kinect.Toolkit.Input;
using Microsoft.Kinect.Wpf.Controls;
using System;
using System.Windows.Controls;

namespace WpfKinectV2CustomButton
{
    /// <summary>
    /// Kinect v2 custom button to support the hand enter and hand leave events
    /// </summary>
    public class KinectV2CustomButton : Button, IKinectControl
    {
        public event EventHandler HandPointerEnter;
        public event EventHandler HandPointerLeave;
        public IKinectController CreateController(IInputModel inputModel, KinectRegion kinectRegion)
        {
            return new KinectV2CustomButtonController(inputModel, kinectRegion, HandPointerEnter, HandPointerLeave);
        }

        public bool IsManipulatable
        {
            get { return false; }
        }

        public bool IsPressable
        {
            get { return true; }
        }
    }
}

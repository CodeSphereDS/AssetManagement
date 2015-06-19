using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catel.Services;
using MahApps.Metro.Controls;

namespace CodeSphere.CSClasses
{
    public class CSPleaseWaitService : IPleaseWaitService
    {
        private readonly ProgressRing _progressRing;
      
        public CSPleaseWaitService()
        {
            _progressRing = new ProgressRing();
        }
        public void Show()
        {
           this._progressRing.IsActive = true;
           System.Threading.Thread.Sleep(2000);
            
        }
        public void Hide()
        {

            this._progressRing.IsActive = false;
            
        }

        public void Pop()
        {
            throw new NotImplementedException();
        }

        public void Push(string status = "")
        {
            throw new NotImplementedException();
        }
     
        public void Show(PleaseWaitWorkDelegate workDelegate, string status = "")
        {
            
            this.Show();
            if (workDelegate != null)
            {
                Task task = Task.Factory.StartNew(() => workDelegate());
                task.Wait();
            }
        }

        public void Show(string status = "")
        {
            this.Show();            
        }

        public int ShowCounter
        {
            get { throw new NotImplementedException(); }
        }

        public void UpdateStatus(int currentItem, int totalItems, string statusFormat = "")
        {
            throw new NotImplementedException();
        }

        public void UpdateStatus(string status)
        {
            throw new NotImplementedException();
        }
    }
}

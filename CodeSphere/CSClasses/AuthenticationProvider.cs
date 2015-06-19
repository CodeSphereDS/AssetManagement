using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catel.MVVM;
using System.Windows;

namespace CodeSphere.CSClasses
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        /// <summary>
        /// Gets or sets the role the user is currently in.
        /// </summary>
        /// <value>The role.</value>
        public string Role { get; set; }

        public bool CanCommandBeExecuted(ICatelCommand command, object commandParameter)
        {
            return true;
        }

        public bool HasAccessToUIElement(FrameworkElement element, object tag, object authenticationTag)
        {
            var authenticationTagAsString = authenticationTag as string;
            if (authenticationTagAsString != null)
            {
                if (string.Compare(authenticationTagAsString, Role, true) == 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

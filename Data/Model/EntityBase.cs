using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catel.Data;
using System.Runtime.Serialization;

namespace Data.Model
{
    public class EntityBase : ModelBase
    {
        protected EntityBase() { }

        protected EntityBase(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        internal void ClearDirtyFlag()
        {
            IsDirty = false;
        }
    }
}

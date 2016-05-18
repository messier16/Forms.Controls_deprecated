using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messier16.Forms.Uwp.Controls
{
    public static class Messier16Controls
    {
        public static void InitAll()
        {
            CheckboxRenderer.Init();
            RatingBarRenderer.Init();
        }
    }
}

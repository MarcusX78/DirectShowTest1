using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DirectShowLib;
using System.Runtime.InteropServices;

namespace DirectShowTest1
{
    public partial class Form1 : Form
    {

        //https://github.com/MarcusX78/DirectShowTest1.git

        //    https://stackoverflow.com/questions/70185428/c-playing-and-controlling-multiple-sound-files-using-directshow
        //  https://stackoverflow.com/questions/8008179/play-multiple-wav-files-using-directshow-at-the-same-and-record-them-c
        //https://sourceforge.net/p/directshownet/discussion/460697/thread/bda5eb027e/

        public Form1()
        {
            InitializeComponent();
        }

        private IGraphBuilder player_gra;
        private void button1_Click(object sender, EventArgs e)
        {

            object source = null;
            DsDevice[] devices = DsDevice.GetDevicesOfCat(FilterCategory.AudioRendererCategory);          
            DsDevice device = (DsDevice)devices[0];

            Guid iid = typeof(IBaseFilter).GUID;
            device.Mon.BindToObject(null, null, ref iid, out source);



            player_gra = (IGraphBuilder)new FilterGraph();
            player_gra.AddFilter((IBaseFilter)source, "Audio Render1");
            player_gra.RenderFile(@"jazz.wav", null);


            (player_gra as IMediaControl).Run();

            (player_gra as IBasicAudio).put_Volume(0);//[0,-10000]
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (player_gra != null)
            {
                Marshal.ReleaseComObject(player_gra);
                GC.Collect();
            }
        }




        private IGraphBuilder player_gra2;
        private void button3_Click(object sender, EventArgs e)
        {
            object source = null;
            DsDevice[] devices = DsDevice.GetDevicesOfCat(FilterCategory.AudioRendererCategory);
            DsDevice device = (DsDevice)devices[0];

            Guid iid = typeof(IBaseFilter).GUID;
            device.Mon.BindToObject(null, null, ref iid, out source);



            player_gra2 = (IGraphBuilder)new FilterGraph();
            player_gra2.AddFilter((IBaseFilter)source, "Audio Render2");
            player_gra2.RenderFile(@"noise.wav", null);


            (player_gra2 as IMediaControl).Run();

            (player_gra2 as IBasicAudio).put_Volume(0);//[0,-10000]
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (player_gra2 != null)
            {
                Marshal.ReleaseComObject(player_gra2);
                GC.Collect();
            }
        }
    }
}

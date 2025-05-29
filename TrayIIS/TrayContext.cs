using Microsoft.Web.Administration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TrayIIS
{
    public class TrayContext : ApplicationContext
    {
        private NotifyIcon trayIcon;
        private ContextMenuStrip contextMenu;

        public TrayContext()
        {
            contextMenu = CreateContextMenu();

            trayIcon = new NotifyIcon()
            {
                Icon = System.Drawing.SystemIcons.Application,
                ContextMenuStrip = CreateContextMenu(),
                Visible = true,
            };

            trayIcon.MouseClick += TrayIcon_MouseClick;
        }

        private void TrayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenu = CreateContextMenu();
                trayIcon.ContextMenuStrip = contextMenu;
                contextMenu.Show(Cursor.Position);
            }
        }

        private ContextMenuStrip CreateContextMenu()
        {
            var menu = new ContextMenuStrip();

            menu.Items.Add(new ToolStripMenuItem("Tray IIS by Enqbator, LLC"));

            menu.Items.Add(new ToolStripSeparator());

            foreach (var pool in GetAppPools())
            {
                var item = new ToolStripMenuItem(pool);

                var assembly = Assembly.GetExecutingAssembly();
                using (var stream = assembly.GetManifestResourceStream("TrayIIS.recycle.png"))
                {
                    item.Image = new Bitmap(stream);
                }

                //item.Image = System.Drawing.Image.FromFile("recycle.png"); // Optional icon
                item.ImageAlign = ContentAlignment.MiddleCenter;
                item.Click += (s, e) => RecycleAppPool(pool);
                menu.Items.Add(item);
            }

            menu.Items.Add(new ToolStripSeparator());

            var checkboxItem = new ToolStripMenuItem("Show only running App Pools")
            {
                CheckOnClick = true,
                Checked = Properties.Settings.Default.ShowOnlyRunning
            };
            checkboxItem.CheckedChanged += (s, e) =>
            {
                Properties.Settings.Default.ShowOnlyRunning = checkboxItem.Checked;
                Properties.Settings.Default.Save();
            };
            menu.Items.Add(checkboxItem);

            var exitItem = new ToolStripMenuItem("Exit");
            exitItem.Click += (s, e) => ExitThread();
            menu.Items.Add(exitItem);

            return menu;
        }

        private List<string> GetAppPools()
        {
            using (ServerManager serverManager = new ServerManager())
            {
                var appPools = serverManager.ApplicationPools.ToList();

                return Properties.Settings.Default.ShowOnlyRunning
                    ? [.. serverManager.ApplicationPools.Where(w => w.WorkerProcesses.Count > 0).Select(p => p.Name)]
                    : [..appPools.Select(p => p.Name)] ;
            }
        }

        private void RecycleAppPool(string appPoolName)
        {
            using (ServerManager serverManager = new ServerManager())
            {
                var pool = serverManager.ApplicationPools[appPoolName];
                pool.Recycle();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && trayIcon != null)
            {
                trayIcon.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

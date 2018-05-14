using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pelicamon.MapBuilder.Controls {
	public partial class OptionPanel : UserControl {

		private Dictionary<string, List<Option>> options;

		public OptionPanel() {
			InitializeComponent();
			this.options = new Dictionary<string, List<Option>>();
		}

		public Option AddOption(string name, string category, Image icon) {
			bool val = options.TryGetValue(category, out List<Option> cat);
			if (!val) {
				cat = new List<Option>();
				options.Add(category, cat);
			}
			Option option = new Option(name, icon);
			cat.Add(option);
			return option;
		}

		public void UpdateDisplay() {
			this.SuspendLayout();
			this.Width = 0;
			List<KeyValuePair<string, List<Option>>> options = this.options.OrderBy(x => x.Key).ToList();
			foreach (KeyValuePair<string, List<Option>> kvp in options) {
				this.Width += 2;
				foreach (Option o in kvp.Value) {
					this.Width += this.Height;
				} 
			}
		}
	}

	public class Option {
		
		public event Action OnPressed = new Action(() => { });
		public string Name { get; internal set; }
		public Image Icon { get; internal set; }

		public Option(string name, Image icon) {
			this.Name = name;
			this.Icon = icon;
		}

		public void PressOption() {
			if (OnPressed != null)
				OnPressed.Invoke();
		}
	}
}

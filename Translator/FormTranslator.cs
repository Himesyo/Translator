using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Himesyo.Runtime;
using Himesyo.Translation;

namespace Himesyo.DocumentTranslator
{
    public partial class FormTranslator : Form
    {
        public FormTranslator()
        {
            InitializeComponent();
        }

        private void FormTranslator_Load(object sender, EventArgs e)
        {
            listBox1.DataSource = FormMain.Translators;
            //listBox1.DisplayMember = "Name";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ITranslatorType translator = FormMain.TranslatorTypes.FirstOrDefault().Value;
            ICreateArgs createArgs = translator.CreateArgs();
            ICreateArgsEditor editor = translator.GetCreateArgsEditor();
            editor.EditArgs = createArgs;
            editor.ShowControl.Dock = DockStyle.Fill;

            panel1.Controls.Clear();
            panel1.Tag = editor;
            panel1.Controls.Add(editor.ShowControl);
            buttonSave.Tag = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is ITranslator translator)
            {
                FormMain.Translators.Remove(translator);
                FormMain.DeleteTranslator(translator);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (panel1.Tag is ICreateArgsEditor editor)
            {
                ITranslatorType translatorType = FormMain.TranslatorTypes[editor.TypeName];
                ITranslator translator = translatorType.CreateTranslator(editor.EditArgs);
                if (translatorType.Multiple)
                {
                    if (FormMain.Translators.FirstOrDefault(t => translator.Equals(t)) != null)
                    {
                        MessageBox.Show($"已含有具有相同值的翻译器。", "翻译", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    panel1.Controls.Clear();
                    panel1.Tag = null;

                    if (FormMain.Translators.FirstOrDefault(t => t.TypeName == editor.TypeName) != null)
                    {
                        MessageBox.Show($"{editor.TypeName} 类型的对象不能重复创建。", "翻译", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                FormMain.Translators.Add(translator);
            }
        }

        private void listBox1_Format(object sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem != null)
            {
                Type type = e.ListItem.GetType();
                if (!type.IsOverrideToString() && e.ListItem is ITranslator translator)
                {
                    e.Value = $"{translator.TypeName} - {translator.Interval}";
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is ITranslator translator)
            {
                ITranslatorType translatorType = FormMain.TranslatorTypes[translator.TypeName];

                ICreateArgs createArgs = translator.CreateArgs;
                ICreateArgsEditor editor = translatorType.GetCreateArgsEditor();
                editor.EditArgs = createArgs;
                editor.ShowControl.Dock = DockStyle.Fill;

                panel1.Controls.Clear();
                panel1.Tag = editor;
                panel1.Controls.Add(editor.ShowControl);
                buttonSave.Tag = translator;
            }
            else
            {
                panel1.Controls.Clear();
                panel1.Tag = null;
                buttonSave.Tag = null;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (buttonSave.Tag is ITranslator translator)
            {
                FormMain.SaveTranslator(translator);
            }
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is ITranslator translator)
            {
                string text = "This is a piece of text for testing.";
                string result = translator.Translate(text);

                MessageBox.Show($"测试结果：\r\n\r\n原文文本：{text}\r\n翻译结果：{result}", "翻译", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

namespace UI_lab1;

partial class FormMain
{
	/// <summary>
	/// Обязательная переменная конструктора.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	/// Освободить все используемые ресурсы.
	/// </summary>
	/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
	protected override void Dispose (bool disposing)
	{
		if (disposing && (components != null)) {
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Код, автоматически созданный конструктором форм Windows

	/// <summary>
	/// Требуемый метод для поддержки конструктора — не изменяйте 
	/// содержимое этого метода с помощью редактора кода.
	/// </summary>
	private void InitializeComponent ()
	{
		this.button1 = new Button();
		this.label1 = new Label();
		this.SuspendLayout();
		// 
		// button1
		// 
		this.button1.Location = new Point(11, 11);
		this.button1.Margin = new Padding(2);
		this.button1.Name = "button1";
		this.button1.Size = new Size(61, 28);
		this.button1.TabIndex = 0;
		this.button1.Text = "button1";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += this.button1_Click;
		// 
		// label1
		// 
		this.label1.AutoSize = true;
		this.label1.Location = new Point(201, 140);
		this.label1.Margin = new Padding(2, 0, 2, 0);
		this.label1.Name = "label1";
		this.label1.Size = new Size(38, 15);
		this.label1.TabIndex = 1;
		this.label1.Text = "label1";
		// 
		// FormMain
		// 
		this.AutoScaleDimensions = new SizeF(7F, 15F);
		this.AutoScaleMode = AutoScaleMode.Font;
		this.ClientSize = new Size(859, 825);
		this.Controls.Add(this.label1);
		this.Controls.Add(this.button1);
		this.Margin = new Padding(2);
		this.Name = "FormMain";
		this.Text = "Form1";
		this.Load += this.FormMail_Load;
		this.MouseClick += this.FormMain_MouseClick;
		this.MouseMove += this.FormMain_MouseMove;
		this.ResumeLayout(false);
		this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.Button button1;
	private System.Windows.Forms.Label label1;
}


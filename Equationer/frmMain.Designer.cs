namespace Equationer
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtInput = new System.Windows.Forms.TextBox();
            this.lblInput = new System.Windows.Forms.Label();
            this.lblSolution = new System.Windows.Forms.Label();
            this.btnSolve = new System.Windows.Forms.Button();
            this.listSolution = new System.Windows.Forms.ListBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSaveSolution = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.dlgSaveSolution = new System.Windows.Forms.SaveFileDialog();
            this.btnClearList = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtInput
            // 
            this.txtInput.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtInput.Cursor = System.Windows.Forms.Cursors.Cross;
            this.txtInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtInput.Location = new System.Drawing.Point(7, 50);
            this.txtInput.Margin = new System.Windows.Forms.Padding(2);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInput.Size = new System.Drawing.Size(860, 115);
            this.txtInput.TabIndex = 0;
            this.txtInput.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
            this.txtInput.Leave += new System.EventHandler(this.txtInput_Leave);
            // 
            // lblInput
            // 
            this.lblInput.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblInput.Location = new System.Drawing.Point(7, 25);
            this.lblInput.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(860, 25);
            this.lblInput.TabIndex = 2;
            this.lblInput.Text = "Equation:";
            this.lblInput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSolution
            // 
            this.lblSolution.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblSolution.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSolution.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblSolution.Location = new System.Drawing.Point(7, 175);
            this.lblSolution.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSolution.Name = "lblSolution";
            this.lblSolution.Size = new System.Drawing.Size(860, 25);
            this.lblSolution.TabIndex = 3;
            this.lblSolution.Text = "Solution:";
            this.lblSolution.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSolve
            // 
            this.btnSolve.BackColor = System.Drawing.SystemColors.MenuBar;
            this.btnSolve.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSolve.Location = new System.Drawing.Point(767, 610);
            this.btnSolve.Margin = new System.Windows.Forms.Padding(2);
            this.btnSolve.Name = "btnSolve";
            this.btnSolve.Size = new System.Drawing.Size(100, 23);
            this.btnSolve.TabIndex = 4;
            this.btnSolve.Text = "Sol&ve !";
            this.btnSolve.UseVisualStyleBackColor = false;
            this.btnSolve.Click += new System.EventHandler(this.btnSolve_Click);
            // 
            // listSolution
            // 
            this.listSolution.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.listSolution.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.listSolution.FormattingEnabled = true;
            this.listSolution.ItemHeight = 25;
            this.listSolution.Location = new System.Drawing.Point(7, 200);
            this.listSolution.Margin = new System.Windows.Forms.Padding(2);
            this.listSolution.Name = "listSolution";
            this.listSolution.Size = new System.Drawing.Size(860, 404);
            this.listSolution.TabIndex = 5;
            this.listSolution.SelectedIndexChanged += new System.EventHandler(this.listSolution_SelectedIndexChanged);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.MenuBar;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(559, 610);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 23);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSaveSolution
            // 
            this.btnSaveSolution.BackColor = System.Drawing.SystemColors.MenuBar;
            this.btnSaveSolution.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveSolution.Enabled = false;
            this.btnSaveSolution.Location = new System.Drawing.Point(663, 610);
            this.btnSaveSolution.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveSolution.Name = "btnSaveSolution";
            this.btnSaveSolution.Size = new System.Drawing.Size(100, 23);
            this.btnSaveSolution.TabIndex = 7;
            this.btnSaveSolution.Text = "&Save";
            this.btnSaveSolution.UseVisualStyleBackColor = false;
            this.btnSaveSolution.Click += new System.EventHandler(this.btnSaveSolution_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.BackColor = System.Drawing.SystemColors.MenuBar;
            this.btnMoveDown.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMoveDown.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnMoveDown.Location = new System.Drawing.Point(7, 610);
            this.btnMoveDown.Margin = new System.Windows.Forms.Padding(2);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(70, 23);
            this.btnMoveDown.TabIndex = 8;
            this.btnMoveDown.Text = "Down";
            this.btnMoveDown.UseVisualStyleBackColor = false;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.BackColor = System.Drawing.SystemColors.MenuBar;
            this.btnMoveUp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMoveUp.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnMoveUp.Location = new System.Drawing.Point(155, 610);
            this.btnMoveUp.Margin = new System.Windows.Forms.Padding(2);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(70, 23);
            this.btnMoveUp.TabIndex = 9;
            this.btnMoveUp.Text = "Up";
            this.btnMoveUp.UseVisualStyleBackColor = false;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // dlgSaveSolution
            // 
            this.dlgSaveSolution.Filter = "Text File ( *.txt ) | *.txt";
            // 
            // btnClearList
            // 
            this.btnClearList.BackColor = System.Drawing.SystemColors.MenuBar;
            this.btnClearList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearList.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClearList.Location = new System.Drawing.Point(81, 610);
            this.btnClearList.Margin = new System.Windows.Forms.Padding(2);
            this.btnClearList.Name = "btnClearList";
            this.btnClearList.Size = new System.Drawing.Size(70, 23);
            this.btnClearList.TabIndex = 10;
            this.btnClearList.Text = "Clear";
            this.btnClearList.UseVisualStyleBackColor = false;
            this.btnClearList.Click += new System.EventHandler(this.btnClearList_Click);
            // 
            // frmMain
            // 
            this.AcceptButton = this.btnSolve;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(872, 647);
            this.Controls.Add(this.btnClearList);
            this.Controls.Add(this.btnMoveUp);
            this.Controls.Add(this.btnMoveDown);
            this.Controls.Add(this.btnSaveSolution);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.listSolution);
            this.Controls.Add(this.btnSolve);
            this.Controls.Add(this.lblSolution);
            this.Controls.Add(this.lblInput);
            this.Controls.Add(this.txtInput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Equationer";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Label lblInput;
        private System.Windows.Forms.Label lblSolution;
        private System.Windows.Forms.Button btnSolve;
        private System.Windows.Forms.ListBox listSolution;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSaveSolution;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.SaveFileDialog dlgSaveSolution;
        private System.Windows.Forms.Button btnClearList;
    }
}


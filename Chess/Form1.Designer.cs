namespace Chess
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.board = new System.Windows.Forms.DataGridView();
            this.col1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.col2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.col3 = new System.Windows.Forms.DataGridViewImageColumn();
            this.col4 = new System.Windows.Forms.DataGridViewImageColumn();
            this.col5 = new System.Windows.Forms.DataGridViewImageColumn();
            this.col6 = new System.Windows.Forms.DataGridViewImageColumn();
            this.col7 = new System.Windows.Forms.DataGridViewImageColumn();
            this.col8 = new System.Windows.Forms.DataGridViewImageColumn();
            this.turnLabelText = new System.Windows.Forms.Label();
            this.turnLabel = new System.Windows.Forms.Label();
            this.winnerLabel = new System.Windows.Forms.Label();
            this.checkLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.timerLabel = new System.Windows.Forms.Label();
            this.timerBox = new System.Windows.Forms.TextBox();
            this.minuteLabel = new System.Windows.Forms.Label();
            this.mouseMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.resignToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offerADrawToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startButton = new System.Windows.Forms.Button();
            this.drawLabel = new System.Windows.Forms.Label();
            this.drawButton = new System.Windows.Forms.Button();
            this.timeLabel = new System.Windows.Forms.Label();
            this.whiteTimeLabel = new System.Windows.Forms.Label();
            this.blackTimeLabel = new System.Windows.Forms.Label();
            this.whiteTimer = new System.Windows.Forms.Timer(this.components);
            this.blackTimer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.blackTime = new System.Windows.Forms.Label();
            this.whiteTime = new System.Windows.Forms.Label();
            this.resignButton = new System.Windows.Forms.Button();
            this.offerDrawButton = new System.Windows.Forms.Button();
            this.whiteMoveBox = new System.Windows.Forms.TextBox();
            this.blackMoveBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.board)).BeginInit();
            this.mouseMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // board
            // 
            this.board.AllowUserToAddRows = false;
            this.board.AllowUserToDeleteRows = false;
            this.board.AllowUserToResizeColumns = false;
            this.board.AllowUserToResizeRows = false;
            this.board.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.board.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col1,
            this.col2,
            this.col3,
            this.col4,
            this.col5,
            this.col6,
            this.col7,
            this.col8});
            this.board.Location = new System.Drawing.Point(12, 12);
            this.board.Name = "board";
            this.board.RowHeadersWidth = 51;
            this.board.RowTemplate.Height = 24;
            this.board.Size = new System.Drawing.Size(606, 521);
            this.board.TabIndex = 0;
            this.board.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.board_CellClick);
            this.board.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.board_CellMouseClick);
            this.board.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.board_CellMouseMove);
            this.board.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.board_RowHeaderMouseClick);
            // 
            // col1
            // 
            this.col1.HeaderText = "A";
            this.col1.MinimumWidth = 6;
            this.col1.Name = "col1";
            this.col1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.col1.Width = 50;
            // 
            // col2
            // 
            this.col2.HeaderText = "B";
            this.col2.MinimumWidth = 6;
            this.col2.Name = "col2";
            this.col2.Width = 50;
            // 
            // col3
            // 
            this.col3.HeaderText = "C";
            this.col3.MinimumWidth = 6;
            this.col3.Name = "col3";
            this.col3.Width = 50;
            // 
            // col4
            // 
            this.col4.HeaderText = "D";
            this.col4.MinimumWidth = 6;
            this.col4.Name = "col4";
            this.col4.Width = 50;
            // 
            // col5
            // 
            this.col5.HeaderText = "E";
            this.col5.MinimumWidth = 6;
            this.col5.Name = "col5";
            this.col5.Width = 50;
            // 
            // col6
            // 
            this.col6.HeaderText = "F";
            this.col6.MinimumWidth = 6;
            this.col6.Name = "col6";
            this.col6.Width = 50;
            // 
            // col7
            // 
            this.col7.HeaderText = "G";
            this.col7.MinimumWidth = 6;
            this.col7.Name = "col7";
            this.col7.Width = 50;
            // 
            // col8
            // 
            this.col8.HeaderText = "H";
            this.col8.MinimumWidth = 6;
            this.col8.Name = "col8";
            this.col8.Width = 50;
            // 
            // turnLabelText
            // 
            this.turnLabelText.AutoSize = true;
            this.turnLabelText.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.turnLabelText.Location = new System.Drawing.Point(624, 12);
            this.turnLabelText.Name = "turnLabelText";
            this.turnLabelText.Size = new System.Drawing.Size(81, 33);
            this.turnLabelText.TabIndex = 1;
            this.turnLabelText.Text = "Turn: ";
            // 
            // turnLabel
            // 
            this.turnLabel.AutoSize = true;
            this.turnLabel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.turnLabel.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.turnLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.turnLabel.Location = new System.Drawing.Point(696, 12);
            this.turnLabel.Name = "turnLabel";
            this.turnLabel.Size = new System.Drawing.Size(84, 33);
            this.turnLabel.TabIndex = 2;
            this.turnLabel.Text = "White";
            // 
            // winnerLabel
            // 
            this.winnerLabel.AutoSize = true;
            this.winnerLabel.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.winnerLabel.Location = new System.Drawing.Point(621, 466);
            this.winnerLabel.Name = "winnerLabel";
            this.winnerLabel.Size = new System.Drawing.Size(111, 33);
            this.winnerLabel.TabIndex = 3;
            this.winnerLabel.Text = "Winner: ";
            // 
            // checkLabel
            // 
            this.checkLabel.AutoSize = true;
            this.checkLabel.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkLabel.Location = new System.Drawing.Point(621, 433);
            this.checkLabel.Name = "checkLabel";
            this.checkLabel.Size = new System.Drawing.Size(129, 33);
            this.checkLabel.TabIndex = 4;
            this.checkLabel.Text = "In Check: ";
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Comic Sans MS", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(692, 12);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(166, 59);
            this.titleLabel.TabIndex = 5;
            this.titleLabel.Text = "CHESS";
            // 
            // timerLabel
            // 
            this.timerLabel.AutoSize = true;
            this.timerLabel.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timerLabel.Location = new System.Drawing.Point(624, 93);
            this.timerLabel.Name = "timerLabel";
            this.timerLabel.Size = new System.Drawing.Size(145, 33);
            this.timerLabel.TabIndex = 6;
            this.timerLabel.Text = "Time Limit: ";
            // 
            // timerBox
            // 
            this.timerBox.Location = new System.Drawing.Point(766, 101);
            this.timerBox.Name = "timerBox";
            this.timerBox.Size = new System.Drawing.Size(60, 22);
            this.timerBox.TabIndex = 7;
            this.timerBox.Text = "0";
            // 
            // minuteLabel
            // 
            this.minuteLabel.AutoSize = true;
            this.minuteLabel.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minuteLabel.Location = new System.Drawing.Point(832, 98);
            this.minuteLabel.Name = "minuteLabel";
            this.minuteLabel.Size = new System.Drawing.Size(71, 24);
            this.minuteLabel.TabIndex = 8;
            this.minuteLabel.Text = "minutes";
            // 
            // mouseMenu
            // 
            this.mouseMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mouseMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resignToolStripMenuItem,
            this.offerADrawToolStripMenuItem});
            this.mouseMenu.Name = "mouseMenu";
            this.mouseMenu.Size = new System.Drawing.Size(164, 52);
            // 
            // resignToolStripMenuItem
            // 
            this.resignToolStripMenuItem.Name = "resignToolStripMenuItem";
            this.resignToolStripMenuItem.Size = new System.Drawing.Size(163, 24);
            this.resignToolStripMenuItem.Text = "Resign";
            this.resignToolStripMenuItem.Click += new System.EventHandler(this.resignToolStripMenuItem_Click);
            // 
            // offerADrawToolStripMenuItem
            // 
            this.offerADrawToolStripMenuItem.Name = "offerADrawToolStripMenuItem";
            this.offerADrawToolStripMenuItem.Size = new System.Drawing.Size(163, 24);
            this.offerADrawToolStripMenuItem.Text = "Offer a Draw";
            this.offerADrawToolStripMenuItem.Click += new System.EventHandler(this.offerADrawToolStripMenuItem_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(718, 209);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 12;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // drawLabel
            // 
            this.drawLabel.AutoSize = true;
            this.drawLabel.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.drawLabel.Location = new System.Drawing.Point(621, 398);
            this.drawLabel.Name = "drawLabel";
            this.drawLabel.Size = new System.Drawing.Size(169, 33);
            this.drawLabel.TabIndex = 13;
            this.drawLabel.Text = "Accept Draw?";
            // 
            // drawButton
            // 
            this.drawButton.Location = new System.Drawing.Point(796, 408);
            this.drawButton.Name = "drawButton";
            this.drawButton.Size = new System.Drawing.Size(75, 23);
            this.drawButton.TabIndex = 14;
            this.drawButton.Text = "Accept";
            this.drawButton.UseVisualStyleBackColor = true;
            this.drawButton.Click += new System.EventHandler(this.drawButton_Click);
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLabel.Location = new System.Drawing.Point(696, 89);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(124, 33);
            this.timeLabel.TabIndex = 15;
            this.timeLabel.Text = "Time Left";
            // 
            // whiteTimeLabel
            // 
            this.whiteTimeLabel.AutoSize = true;
            this.whiteTimeLabel.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.whiteTimeLabel.Location = new System.Drawing.Point(621, 126);
            this.whiteTimeLabel.Name = "whiteTimeLabel";
            this.whiteTimeLabel.Size = new System.Drawing.Size(98, 33);
            this.whiteTimeLabel.TabIndex = 16;
            this.whiteTimeLabel.Text = "White: ";
            // 
            // blackTimeLabel
            // 
            this.blackTimeLabel.AutoSize = true;
            this.blackTimeLabel.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blackTimeLabel.Location = new System.Drawing.Point(624, 159);
            this.blackTimeLabel.Name = "blackTimeLabel";
            this.blackTimeLabel.Size = new System.Drawing.Size(89, 33);
            this.blackTimeLabel.TabIndex = 17;
            this.blackTimeLabel.Text = "Black: ";
            // 
            // whiteTimer
            // 
            this.whiteTimer.Interval = 1000;
            this.whiteTimer.Tick += new System.EventHandler(this.whiteTimer_Tick);
            // 
            // blackTimer
            // 
            this.blackTimer.Interval = 1000;
            this.blackTimer.Tick += new System.EventHandler(this.blackTimer_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(719, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 33);
            this.label1.TabIndex = 18;
            // 
            // blackTime
            // 
            this.blackTime.AutoSize = true;
            this.blackTime.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blackTime.Location = new System.Drawing.Point(712, 159);
            this.blackTime.Name = "blackTime";
            this.blackTime.Size = new System.Drawing.Size(0, 33);
            this.blackTime.TabIndex = 19;
            // 
            // whiteTime
            // 
            this.whiteTime.AutoSize = true;
            this.whiteTime.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.whiteTime.Location = new System.Drawing.Point(718, 126);
            this.whiteTime.Name = "whiteTime";
            this.whiteTime.Size = new System.Drawing.Size(0, 33);
            this.whiteTime.TabIndex = 20;
            // 
            // resignButton
            // 
            this.resignButton.Location = new System.Drawing.Point(657, 504);
            this.resignButton.Name = "resignButton";
            this.resignButton.Size = new System.Drawing.Size(75, 23);
            this.resignButton.TabIndex = 21;
            this.resignButton.Text = "Resign";
            this.resignButton.UseVisualStyleBackColor = true;
            this.resignButton.Click += new System.EventHandler(this.resignButton_Click);
            // 
            // offerDrawButton
            // 
            this.offerDrawButton.Location = new System.Drawing.Point(756, 504);
            this.offerDrawButton.Name = "offerDrawButton";
            this.offerDrawButton.Size = new System.Drawing.Size(102, 23);
            this.offerDrawButton.TabIndex = 22;
            this.offerDrawButton.Text = "Offer a Draw";
            this.offerDrawButton.UseVisualStyleBackColor = true;
            this.offerDrawButton.Click += new System.EventHandler(this.offerDrawButton_Click);
            // 
            // whiteMoveBox
            // 
            this.whiteMoveBox.Location = new System.Drawing.Point(627, 195);
            this.whiteMoveBox.Multiline = true;
            this.whiteMoveBox.Name = "whiteMoveBox";
            this.whiteMoveBox.ReadOnly = true;
            this.whiteMoveBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.whiteMoveBox.Size = new System.Drawing.Size(105, 200);
            this.whiteMoveBox.TabIndex = 23;
            // 
            // blackMoveBox
            // 
            this.blackMoveBox.Location = new System.Drawing.Point(756, 195);
            this.blackMoveBox.Multiline = true;
            this.blackMoveBox.Name = "blackMoveBox";
            this.blackMoveBox.ReadOnly = true;
            this.blackMoveBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.blackMoveBox.Size = new System.Drawing.Size(105, 200);
            this.blackMoveBox.TabIndex = 24;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 539);
            this.Controls.Add(this.blackMoveBox);
            this.Controls.Add(this.whiteMoveBox);
            this.Controls.Add(this.offerDrawButton);
            this.Controls.Add(this.resignButton);
            this.Controls.Add(this.whiteTime);
            this.Controls.Add(this.blackTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.blackTimeLabel);
            this.Controls.Add(this.whiteTimeLabel);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.drawButton);
            this.Controls.Add(this.drawLabel);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.minuteLabel);
            this.Controls.Add(this.timerBox);
            this.Controls.Add(this.timerLabel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.checkLabel);
            this.Controls.Add(this.winnerLabel);
            this.Controls.Add(this.turnLabel);
            this.Controls.Add(this.turnLabelText);
            this.Controls.Add(this.board);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.board)).EndInit();
            this.mouseMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView board;
        private System.Windows.Forms.Label turnLabelText;
        private System.Windows.Forms.Label turnLabel;
        private System.Windows.Forms.DataGridViewImageColumn col1;
        private System.Windows.Forms.DataGridViewImageColumn col2;
        private System.Windows.Forms.DataGridViewImageColumn col3;
        private System.Windows.Forms.DataGridViewImageColumn col4;
        private System.Windows.Forms.DataGridViewImageColumn col5;
        private System.Windows.Forms.DataGridViewImageColumn col6;
        private System.Windows.Forms.DataGridViewImageColumn col7;
        private System.Windows.Forms.DataGridViewImageColumn col8;
        private System.Windows.Forms.Label winnerLabel;
        private System.Windows.Forms.Label checkLabel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label timerLabel;
        private System.Windows.Forms.TextBox timerBox;
        private System.Windows.Forms.Label minuteLabel;
        private System.Windows.Forms.ContextMenuStrip mouseMenu;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.ToolStripMenuItem resignToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem offerADrawToolStripMenuItem;
        private System.Windows.Forms.Label drawLabel;
        private System.Windows.Forms.Button drawButton;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Label whiteTimeLabel;
        private System.Windows.Forms.Label blackTimeLabel;
        private System.Windows.Forms.Timer whiteTimer;
        private System.Windows.Forms.Timer blackTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label blackTime;
        private System.Windows.Forms.Label whiteTime;
        private System.Windows.Forms.Button resignButton;
        private System.Windows.Forms.Button offerDrawButton;
        private System.Windows.Forms.TextBox whiteMoveBox;
        private System.Windows.Forms.TextBox blackMoveBox;
    }
}


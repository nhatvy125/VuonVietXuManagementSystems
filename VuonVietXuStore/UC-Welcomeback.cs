using System;
using System.Drawing;
using System.Windows.Forms;

namespace VuonVietXuStore
{
    public partial class UC_Welcomeback : UserControl
    {
        // Khai báo các màu sắc chủ đạo theo tone Thực phẩm sạch
        private Color colorDefaultBg = Color.White;
        private Color colorHoverBg = Color.FromArgb(235, 247, 238); // Xanh lá cực nhẹ khi hover card
        private Color colorBorderDefault = Color.FromArgb(220, 230, 222);

        // Quản lý trạng thái Animation phóng to thu nhỏ của Card
        private Timer animationTimer = new Timer();
        private Panel activeCard = null;
        private bool isExpanding = true;
        private int targetWidth, targetHeight, targetX, targetY;

        public UC_Welcomeback()
        {
            InitializeComponent();

            // Bật DoubleBuffered để tránh hiện tượng màn hình bị giật/nháy
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.UpdateStyles();

            SetupCardEffects();
            InitAnimation();
        }

        // 1. XỬ LÝ CO GIÃN TỰ ĐỘNG THEO MÀN HÌNH (RESPONSIVE)
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (panelCards == null || card1 == null) return;

            panelCards.Width = (int)(this.Width * 0.92);
            panelCards.Left = (this.Width - panelCards.Width) / 2;

            int totalSpacing = 60;
            int cardWidth = (panelCards.Width - totalSpacing) / 4;
            int cardHeight = 160;

            card1.Size = new Size(cardWidth, cardHeight);
            card2.Size = new Size(cardWidth, cardHeight);
            card3.Size = new Size(cardWidth, cardHeight);
            card4.Size = new Size(cardWidth, cardHeight);

            card1.Left = 0;
            card2.Left = cardWidth + 20;
            card3.Left = (cardWidth * 2) + 40;
            card4.Left = (cardWidth * 3) + 60;

            CenterControlsInCard(card1, lblCard1Icon, lblCard1Val, lblCard1Title);
            CenterControlsInCard(card2, lblCard2Icon, lblCard2Val, lblCard2Title);
            CenterControlsInCard(card3, lblCard3Icon, lblCard3Val, lblCard3Title);
            CenterControlsInCard(card4, lblCard4Icon, lblCard4Val, lblCard4Title);
        }

        private void CenterControlsInCard(Panel card, Label icon, Label val, Label title)
        {
            if (icon == null || val == null || title == null) return;
            icon.Left = (card.Width - icon.Width) / 2;
            val.Left = (card.Width - val.Width) / 2;
            title.Left = (card.Width - title.Width) / 2;
        }

        // 2. TẠO HIỆU ỨNG HOVER
        private void SetupCardEffects()
        {
            Panel[] cards = { card1, card2, card3, card4 };
            foreach (var card in cards)
            {
                card.Cursor = Cursors.Hand;
                card.MouseEnter += Card_MouseEnter;
                card.MouseLeave += Card_MouseLeave;

                foreach (Control child in card.Controls)
                {
                    child.Cursor = Cursors.Hand;
                    child.MouseEnter += (s, e) => Card_MouseEnter(card, e);
                    child.MouseLeave += (s, e) => Card_MouseLeave(card, e);
                }
            }
        }

        private void Card_MouseEnter(object sender, EventArgs e)
        {
            Panel card = sender as Panel;
            if (card != null)
            {
                card.BackColor = colorHoverBg;
                StartCardAnimation(card, true);
            }
        }

        private void Card_MouseLeave(object sender, EventArgs e)
        {
            Panel card = sender as Panel;
            if (card != null)
            {
                card.BackColor = colorDefaultBg;
                StartCardAnimation(card, false);
            }
        }

        private void lblDate_Click(object sender, EventArgs e)
        {

        }

        private void lblWelcome_Click(object sender, EventArgs e)
        {

        }

        // 3. HIỆU ỨNG ANIMATION TIMER
        private void InitAnimation()
        {
            animationTimer.Interval = 10;
            animationTimer.Tick += AnimationTimer_Tick;
        }

        private void StartCardAnimation(Panel card, bool expand)
        {
            animationTimer.Stop();
            activeCard = card;
            isExpanding = expand;

            int baseWidth = (panelCards.Width - 60) / 4;
            int baseLeft = 0;
            if (card == card2) baseLeft = baseWidth + 20;
            if (card == card3) baseLeft = (baseWidth * 2) + 40;
            if (card == card4) baseLeft = (baseWidth * 3) + 60;

            if (expand)
            {
                targetWidth = baseWidth + 8;
                targetHeight = 166;
                targetX = baseLeft - 4;
                targetY = -3;
            }
            else
            {
                targetWidth = baseWidth;
                targetHeight = 160;
                targetX = baseLeft;
                targetY = 0;
            }

            animationTimer.Start();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (activeCard == null) return;
            int stepSize = 2;

            if (activeCard.Width != targetWidth)
                activeCard.Width += (activeCard.Width < targetWidth) ? stepSize : -stepSize;

            if (activeCard.Height != targetHeight)
                activeCard.Height += (activeCard.Height < targetHeight) ? stepSize : -stepSize;

            if (activeCard.Left != targetX)
                activeCard.Left += (activeCard.Left < targetX) ? 1 : -1;

            if (activeCard.Top != targetY)
                activeCard.Top += (activeCard.Top < targetY) ? 1 : -1;

            if (activeCard.Width == targetWidth && activeCard.Height == targetHeight && activeCard.Top == targetY)
            {
                animationTimer.Stop();
            }
        }
    }
}
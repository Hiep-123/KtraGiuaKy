using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace De02
{
    public partial class frmSanpham : Form
    {
        public frmSanpham()
        {
            InitializeComponent();
            LoadDaTa();
            LoadComboBox();
        }
        QLSanPhamContext context =  new QLSanPhamContext();
        List<ViewModel> listView = new List<ViewModel>();
        public void LoadDaTa()
        {
            listView.Clear();
            var lstLoaiSP = context.LoaiSPs.ToList();
            var lstSP = context.SanPhams.ToList();
            foreach (var item in lstSP)
            {
                ViewModel vm = new ViewModel();
                vm.MaSP = item.MaSP;
                vm.TenSP = item.TenSP;  
                vm.Ngaynhap = item.Ngaynhap;    
                vm.TenLoai = item.LoaiSP.TenLoai;
                listView.Add(vm);   
            }
            dtgSanPham.DataSource = null;
            dtgSanPham.DataSource = listView;
        }
        public void LoadComboBox()
        {
            var tenSP = context.LoaiSPs.ToList();
            cboLoaiSP.DataSource = tenSP;
            cboLoaiSP.DisplayMember = "TenLoai";
            cboLoaiSP.ValueMember = "MaLoai";
        }

        private void dtgSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) { return; }
            DataGridViewRow row = dtgSanPham.Rows[e.RowIndex];
            if (row != null)
            {
                txtMaSP.Text = row.Cells[0].Value.ToString();
                txtTenSP.Text = row.Cells[1].Value.ToString();
                dtNgaynhap.Text = row.Cells[2].Value.ToString();
                cboLoaiSP.Text = row.Cells[3].Value.ToString(); 
            }
        }

        public void RefeshTextBox()
        {
            txtMaSP.Clear();
            txtTenSP.Clear();
            cboLoaiSP.SelectedIndex = -1;
            dtNgaynhap.Text = "";
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            
            if (context.SanPhams.Any(s => s.MaLoai == txtMaSP.Text))
            {
                MessageBox.Show("Mã sản phẩm đã tồn tại!");
                return;
            }
            var sanPham = new SanPham
            {
                MaSP = txtMaSP.Text,
                TenSP = txtTenSP.Text,
                Ngaynhap = dtNgaynhap.Value,
                MaLoai = cboLoaiSP.SelectedValue.ToString(),    
            };
            context.SanPhams.Add(sanPham);
            context.SaveChanges();
            MessageBox.Show("Thêm mới dữ liệu thành công!");
            RefeshTextBox();
            LoadDaTa();
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            var existingSanPham = context.SanPhams.FirstOrDefault(s => s.MaSP == txtMaSP.Text);
            var sanPhamID = dtgSanPham.SelectedRows[0].Cells[0].Value.ToString();
            SanPham updateSanPham = context.SanPhams.FirstOrDefault(s => s.MaSP == sanPhamID);
            if (existingSanPham == null)
            {
                MessageBox.Show("Không tìm thấy mã sản phẩm cần sửa!");
                return;
            }
            if (sanPhamID != null)
            {
                updateSanPham.MaSP = txtMaSP.Text;
                updateSanPham.TenSP = txtTenSP.Text;
                updateSanPham.Ngaynhap = dtNgaynhap.Value;
                updateSanPham.MaLoai = cboLoaiSP.SelectedValue.ToString();
            }
            context.SaveChanges();
            MessageBox.Show("Cập nhật dữ liệu thành công!");
            RefeshTextBox();
            LoadDaTa();
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            var sanPhamID = dtgSanPham.SelectedRows[0].Cells[0].Value.ToString();
            var sanPham = context.SanPhams.FirstOrDefault(u => u.MaSP == sanPhamID);
            if (sanPham == null)
            {
                MessageBox.Show("Không tìm thấy mã sản phẩm cần xóa!");
                return;
            }
            var confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này ?", "Xác nhận xóa", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                context.SanPhams.Remove(sanPham);
                context.SaveChanges();
                MessageBox.Show("Xóa sản phẩm thành công !");
                RefeshTextBox();
                LoadDaTa();
            }
        }

        private void btTimKiem_Click(object sender, EventArgs e)
        {
            string searchText = txtTimkiem.Text.Trim();
            if (!string.IsNullOrEmpty(searchText))
            {
                listView.Clear();
                var lstLoaiSP = context.LoaiSPs.ToList();
                var lstSP = context.SanPhams.Where(s => s.TenSP.Contains(searchText)).ToList();
                foreach (var item in lstSP)
                {
                    ViewModel vm = new ViewModel();
                    vm.MaSP = item.MaSP;
                    vm.TenSP = item.TenSP;
                    vm.Ngaynhap = item.Ngaynhap;
                    vm.TenLoai = lstLoaiSP.Where(u => u.MaLoai == item.MaLoai).FirstOrDefault().TenLoai; ;
                    listView.Add(vm);
                }
                dtgSanPham.DataSource = null;
                dtgSanPham.DataSource = listView;
            }
            else
            {
                MessageBox.Show("Vui lòng nhập thông tin tìm kiếm.");
                return;
            }
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit(); // Thoát chương trình
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            RefeshTextBox();
            LoadDaTa();
        }
    }
}

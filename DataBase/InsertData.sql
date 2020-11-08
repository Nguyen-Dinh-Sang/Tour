USE TourDB
GO

-- Loại tour
INSERT INTO Tour_Loai(Loai_Ten, Loai_MoTa)
VALUES(N'Du lịch tham quan', N'Tham quan di tích lịch sử, thắng cảnh')

INSERT INTO Tour_Loai(Loai_Ten, Loai_MoTa)
VALUES(N'Du lịch văn hóa', N'Du lịch lễ hội, du lịch hoa')

INSERT INTO Tour_Loai(Loai_Ten, Loai_MoTa)
VALUES(N'Du lịch ẩm thực', N'Thưởng thức những bữa tiệc cung đình Huế hay ẩm thực Bắc Trung Nam')

INSERT INTO Tour_Loai(Loai_Ten, Loai_MoTa)
VALUES(N'Du lịch xanh', N'Hướng về thiên nhiên, du lịch sinh thái, du lịch nghỉ dưỡng và chữa bệnh')

INSERT INTO Tour_Loai(Loai_Ten, Loai_MoTa)
VALUES(N'Du lịch MICE', N'Gặp gỡ xúc tiến, hội nghị, hội thảo, du lịch chuyên đề')

INSERT INTO Tour_Loai(Loai_Ten, Loai_MoTa)
VALUES(N'Teambuilding', N'Kết hợp du lịch tham quan, nghĩ dưỡng với các chương trình Team')
GO

-- Các địa điểm
INSERT INTO Tour_DiaDiem(DiaDiem_ThanhPho, DiaDiem_Ten, DiaDiem_MoTa)
VALUES(N'Thủ Đô Hà Nội', N'Cầu Thê Húc', N'Cầu Thê Húc cong cong hình đuôi tôm')

INSERT INTO Tour_DiaDiem(DiaDiem_ThanhPho, DiaDiem_Ten, DiaDiem_MoTa)
VALUES(N'Thủ Đô Hà Nội', N'Hồ Hoàn Kiếm', N'Nơi Lê Lợi trả kiếm cho rùa thần')

INSERT INTO Tour_DiaDiem(DiaDiem_ThanhPho, DiaDiem_Ten, DiaDiem_MoTa)
VALUES(N'Thủ Đô Hà Nội', N'Lăng Bác', N'Quảng trường Ba Đình')

INSERT INTO Tour_DiaDiem(DiaDiem_ThanhPho, DiaDiem_Ten, DiaDiem_MoTa)
VALUES(N'Thủ Đô Hà Nội', N'Đền Ngọc Sơn', N'Nên thăm quan vào ban đêm để xem vẻ đẹp rực rỡ')

INSERT INTO Tour_DiaDiem(DiaDiem_ThanhPho, DiaDiem_Ten, DiaDiem_MoTa)
VALUES(N'Thủ Đô Hà Nội', N'Nhà hát lớn Hà Nội', N'Cổ kính, thơ mộng')

INSERT INTO Tour_DiaDiem(DiaDiem_ThanhPho, DiaDiem_Ten, DiaDiem_MoTa)
VALUES(N'Sa Pa', N'Ruộng bậc thang', N'Vẻ đẹp ấn tượng của ruộng bậc thang Sa Pa thuộc top 7 ruộng bậc thang đẹp nhất châu Á và thế giới do tạp chí Travel and Leisure (Mỹ) bình chọn (2009) và được Bộ Văn hóa – Thể thao và Du lịch xếp hạng là di sản danh thắng Quốc gia vào tháng 11 năm 2013')

INSERT INTO Tour_DiaDiem(DiaDiem_ThanhPho, DiaDiem_Ten, DiaDiem_MoTa)
VALUES(N'Sa Pa', N'Thung lũng Mường Hoa', N'Phong cảnh hữu tình')

INSERT INTO Tour_DiaDiem(DiaDiem_ThanhPho, DiaDiem_Ten, DiaDiem_MoTa)
VALUES(N'Sa Pa', N'Chợ phiên Bắc Hà', N'Nét đẹp văn hoá vùng núi Việt Nam')

INSERT INTO Tour_DiaDiem(DiaDiem_ThanhPho, DiaDiem_Ten, DiaDiem_MoTa)
VALUES(N'Hà Giang', N'Đèo Mã Pí Lèng', N'Ngoạn mục nổi tiếng ở Hà Giang')

INSERT INTO Tour_DiaDiem(DiaDiem_ThanhPho, DiaDiem_Ten, DiaDiem_MoTa)
VALUES(N'Ninh Bình', N'Khu du lịch sinh thái Tràng An – Ninh Bình', N'Vẻ đẹp bất tận của núi non, sông nước, khám phá những hang động kì diệu')

INSERT INTO Tour_DiaDiem(DiaDiem_ThanhPho, DiaDiem_Ten, DiaDiem_MoTa)
VALUES(N'Quảng Ninh', N'Vịnh Hạ Long', N'Hàng trăm hòn đảo lớn nhỏ với những hòn đảo đá vôi, hệ thống núi đá và hang động, Vịnh Hạ Long là niềm tự hào của Việt Nam lọt vào top 29 Vịnh đẹp nhất thế giới và được UNESCO hai lần công nhận là di sản thiên nhiên thế giới.')

INSERT INTO Tour_DiaDiem(DiaDiem_ThanhPho, DiaDiem_Ten, DiaDiem_MoTa)
VALUES(N'Quảng Bình', N'Phong Nha – Kẻ Bảng', N'Cảnh quan thơ mộng, hệ thống hang động vô cùng ấn tượng, sông núi bạt ngàn, nhiều loài động thực vật quý hiếm')

INSERT INTO Tour_DiaDiem(DiaDiem_ThanhPho, DiaDiem_Ten, DiaDiem_MoTa)
VALUES(N'Thừa Thiên Huế', N'Cố đô Huế ', N'uế là thành phố yên bình, cổ kính với những di sản, công trình kiến trúc mang đậm dấu ấn lịch sử nước nhà')

INSERT INTO Tour_DiaDiem(DiaDiem_ThanhPho, DiaDiem_Ten, DiaDiem_MoTa)
VALUES(N'Thừa Thiên Huế', N'Chùa Thiên Mụ', N'Vẻ đẹp trầm mặc, cổ xưa')

INSERT INTO Tour_DiaDiem(DiaDiem_ThanhPho, DiaDiem_Ten, DiaDiem_MoTa)
VALUES(N'Quảng Nam', N'Phố cổ Hội An', N'Phố cổ Hội An là chốn yên bình, nên thơ, nằm e ấp, dịu dàng bên dòng sông Hoài')

INSERT INTO Tour_DiaDiem(DiaDiem_ThanhPho, DiaDiem_Ten, DiaDiem_MoTa)
VALUES(N'Khánh Hòa', N'Nha Trang', N'Ngoài cảnh quan thiên nhiên trong lành, mát mẻ với những hòn đảo đẹp, hệ sinh thái san hô, Nha Trang còn là vùng đất nổi tiếng của món yến sào Khánh Hoà bổ dưỡng, cao cấp')

INSERT INTO Tour_DiaDiem(DiaDiem_ThanhPho, DiaDiem_Ten, DiaDiem_MoTa)
VALUES(N'Bình Thuận', N'Mũi Né – Phan Thiết', N'Nơi đây là một bờ biển xanh ngắt trải dài, hoang vu như sa mạc, những còn cát đỏ tuyệt đẹp khi hoàng hôn buông xuống')

INSERT INTO Tour_DiaDiem(DiaDiem_ThanhPho, DiaDiem_Ten, DiaDiem_MoTa)
VALUES(N'Lâm Đồng', N'Đà Lạt', N'Đà Lạt là một thành phố mộng mơ, huyền ảo trong làn sương mù trắng xoá và luôn ngập tràn muôn sắc hoa. Khí hậu ôn hoà, ấm áp của Đà Lạt cũng là một trong những lí do lôi cuốn khách du lịch tới tham quan, du lịch và nghỉ dưỡng')

INSERT INTO Tour_DiaDiem(DiaDiem_ThanhPho, DiaDiem_Ten, DiaDiem_MoTa)
VALUES(N'Thành phố Hồ Chí Minh', N'Thành phố Hồ Chí Minh', N'Thành phố Hồ Chí Minh là trung tâm kinh tế – văn hoá lớn nhất Việt Nam')

INSERT INTO Tour_DiaDiem(DiaDiem_ThanhPho, DiaDiem_Ten, DiaDiem_MoTa)
VALUES(N'Kiên Giang', N'Đảo Phú Quốc', N'Là hòn đảo lớn nhất Việt Nam. Nằm ở Vịnh Thái Lan, đảo Phú Quốc là nơi du lịch nghỉ dưỡng lý tưởng với những khu rừng nhiệt đới nguyên sơ và các bãi biển cát trắng trải dài')

INSERT INTO Tour_DiaDiem(DiaDiem_ThanhPho, DiaDiem_Ten, DiaDiem_MoTa)
VALUES(N'Bình Phước', N'Rừng cao su Bình Phước', N'Nơi đây chỉ có muỗi và kiến')

INSERT INTO Tour_DiaDiem(DiaDiem_ThanhPho, DiaDiem_Ten, DiaDiem_MoTa)
VALUES(N'Đồng Nai', N'Khu du lịch Thác Mơ', N'Phong cảnh hữu tình và thơ mộng')

INSERT INTO Tour_DiaDiem(DiaDiem_ThanhPho, DiaDiem_Ten, DiaDiem_MoTa)
VALUES(N'Bà Rịa - Vũng Tàu', N'Thành phố biển Vũng Tàu', N'Địa hình đa dạng và sở hữu nhiều địa điểm đẹp')
GO

-- Tour
INSERT INTO Tour(Tour_Ten, Tour_MoTa, Loai_ID)
VALUES(N'Tây Bắc', N'Du lịch các tỉnh vùng núi phía bắc Việt Nam', 1)

INSERT INTO Tour(Tour_Ten, Tour_MoTa, Loai_ID)
VALUES(N'Biển Đảo', N'Tham quan các vùng biển, các hòn đảo từ bắc vào nam', 2)

INSERT INTO Tour(Tour_Ten, Tour_MoTa, Loai_ID)
VALUES(N'Miền Trung', N'Khám phá các vịnh biển và hang động nổi tiếng', 3)

INSERT INTO Tour(Tour_Ten, Tour_MoTa, Loai_ID)
VALUES(N'Miền Nam', N'Vòng quanh các tỉnh Đông Nam Bộ', 4)
GO

-- Giá tour
INSERT INTO Tour_Gia(Gia_SoTien, Tour_ID, Gia_TuNgay, Gia_DenNgay)
VALUES(1000000, 1, '2020-01-01', '2020-04-01')

INSERT INTO Tour_Gia(Gia_SoTien, Tour_ID, Gia_TuNgay, Gia_DenNgay)
VALUES(1000000, 2, '2020-04-01', '2020-07-01')

INSERT INTO Tour_Gia(Gia_SoTien, Tour_ID, Gia_TuNgay, Gia_DenNgay)
VALUES(1000000, 3, '2020-07-01', '2020-10-01')

INSERT INTO Tour_Gia(Gia_SoTien, Tour_ID, Gia_TuNgay, Gia_DenNgay)
VALUES(1000000, 4, '2020-10-01', '2020-12-31')
GO

-- Giá tại một thời điểm
INSERT INTO Gia_Tour_HienTai(Tour_ID, Gia_ID)
VALUES(1,1)

INSERT INTO Gia_Tour_HienTai(Tour_ID, Gia_ID)
VALUES(2,2)

INSERT INTO Gia_Tour_HienTai(Tour_ID, Gia_ID)
VALUES(3,3)

INSERT INTO Gia_Tour_HienTai(Tour_ID, Gia_ID)
VALUES(4,4)
GO

-- Chi tiết tour
INSERT INTO Tour_ChiTiet(Tour_ID, DiaDiem_ID, ChiTiet_ThuTu)
VALUES(1, 6, 1)

INSERT INTO Tour_ChiTiet(Tour_ID, DiaDiem_ID, ChiTiet_ThuTu)
VALUES(1, 7, 2)

INSERT INTO Tour_ChiTiet(Tour_ID, DiaDiem_ID, ChiTiet_ThuTu)
VALUES(1, 9, 3)

INSERT INTO Tour_ChiTiet(Tour_ID, DiaDiem_ID, ChiTiet_ThuTu)
VALUES(1, 10, 4)

INSERT INTO Tour_ChiTiet(Tour_ID, DiaDiem_ID, ChiTiet_ThuTu)
VALUES(2, 20, 2)

INSERT INTO Tour_ChiTiet(Tour_ID, DiaDiem_ID, ChiTiet_ThuTu)
VALUES(2, 23, 3)

INSERT INTO Tour_ChiTiet(Tour_ID, DiaDiem_ID, ChiTiet_ThuTu)
VALUES(2, 16, 1)

INSERT INTO Tour_ChiTiet(Tour_ID, DiaDiem_ID, ChiTiet_ThuTu)
VALUES(3, 13, 1)

INSERT INTO Tour_ChiTiet(Tour_ID, DiaDiem_ID, ChiTiet_ThuTu)
VALUES(3, 15, 2)

INSERT INTO Tour_ChiTiet(Tour_ID, DiaDiem_ID, ChiTiet_ThuTu)
VALUES(3, 16, 3)

INSERT INTO Tour_ChiTiet(Tour_ID, DiaDiem_ID, ChiTiet_ThuTu)
VALUES(3, 17, 4)

INSERT INTO Tour_ChiTiet(Tour_ID, DiaDiem_ID, ChiTiet_ThuTu)
VALUES(4, 19, 1)

INSERT INTO Tour_ChiTiet(Tour_ID, DiaDiem_ID, ChiTiet_ThuTu)
VALUES(4, 23, 2)
GO

-- Đoàn đi tour
INSERT INTO Tour_Doan(Tour_ID, Doan_Ten, Doan_NgayDi, Doan_NgayVe, Doan_ChiTiet, Doan_GiaTour)
VALUES(1, N'Đoàn 1', '2020-10-30', '2020-11-10', N'Đoàn 1', 1000000)

INSERT INTO Tour_Doan(Tour_ID, Doan_Ten, Doan_NgayDi, Doan_NgayVe, Doan_ChiTiet, Doan_GiaTour)
VALUES(2, N'Đoàn 2', '2020-10-30', '2020-11-10', N'Đoàn 2', 1000000)

INSERT INTO Tour_Doan(Tour_ID, Doan_Ten, Doan_NgayDi, Doan_NgayVe, Doan_ChiTiet, Doan_GiaTour)
VALUES(3, N'Đoàn 3', '2020-10-30', '2020-11-10', N'Đoàn 3', 1000000)

INSERT INTO Tour_Doan(Tour_ID, Doan_Ten, Doan_NgayDi, Doan_NgayVe, Doan_ChiTiet, Doan_GiaTour)
VALUES(4, N'Đoàn 4', '2020-10-30', '2020-11-10', N'Đoàn 4', 1000000)
GO

-- Nhân viên
INSERT INTO Tour_NhanVien(NhanVien_Ten, NhanVien_SoDienThoai, NhanVien_Email, NhanVien_NgaySinh)
VALUES(N'Nguyễn Đình Sang', '0345225651', 'nguyendinhsang9199@gmail.com', '1999-01-09')

INSERT INTO Tour_NhanVien(NhanVien_Ten, NhanVien_SoDienThoai, NhanVien_Email, NhanVien_NgaySinh)
VALUES(N'Nguyễn Thị Xuân Linh', '0333748913', 'xuanlinh99.249199@gmail.com', '1999-10-24')

INSERT INTO Tour_NhanVien(NhanVien_Ten, NhanVien_SoDienThoai, NhanVien_Email, NhanVien_NgaySinh)
VALUES(N'Trần Thanh Phong', '0275138858', 'phongle11303@gmail.com', '2000-01-01')

INSERT INTO Tour_NhanVien(NhanVien_Ten, NhanVien_SoDienThoai, NhanVien_Email, NhanVien_NgaySinh)
VALUES(N'Phạm Phương Thanh', '0355773700', 'khachehoa@gmail.com', '1998-01-01')
GO

-- Khách hàng
INSERT INTO Tour_KhachHang(KhachHang_Ten, KhachHang_SoDienThoai, KhachHang_Email, KhachHang_NgaySinh, KhachHang_ChungMinhNhanDan)
VALUES(N'Phạm Phương Thanh', '0355773700', 'khachehoa@gmail.com', '1998-01-01', '123456789')

INSERT INTO Tour_KhachHang(KhachHang_Ten, KhachHang_SoDienThoai, KhachHang_Email, KhachHang_NgaySinh, KhachHang_ChungMinhNhanDan)
VALUES(N'Trần Thanh Phong', '0275138858', 'phongle11303@gmail.com', '2000-01-01', '987654321')

INSERT INTO Tour_KhachHang(KhachHang_Ten, KhachHang_SoDienThoai, KhachHang_Email, KhachHang_NgaySinh, KhachHang_ChungMinhNhanDan)
VALUES(N'Nguyễn Thị Xuân Linh', '0333748913', 'xuanlinh99.249199@gmail.com', '1999-10-24', '7531598462')

INSERT INTO Tour_KhachHang(KhachHang_Ten, KhachHang_SoDienThoai, KhachHang_Email, KhachHang_NgaySinh, KhachHang_ChungMinhNhanDan)
VALUES(N'Nguyễn Đình Sang', '0345225651', 'nguyendinhsang9199@gmail.com', '1999-01-09', '741852963')
GO

-- Khách hàng trong đoàn du lịch
INSERT INTO Doan_KhachHang(KhachHang_ID, Doan_ID)
VALUES(1, 1)

INSERT INTO Doan_KhachHang(KhachHang_ID, Doan_ID)
VALUES(2, 1)

INSERT INTO Doan_KhachHang(KhachHang_ID, Doan_ID)
VALUES(3, 1)

INSERT INTO Doan_KhachHang(KhachHang_ID, Doan_ID)
VALUES(1, 3)

INSERT INTO Doan_KhachHang(KhachHang_ID, Doan_ID)
VALUES(3, 3)

INSERT INTO Doan_KhachHang(KhachHang_ID, Doan_ID)
VALUES(4, 3)
GO

-- Nhân viên phụ trách
INSERT INTO Doan_NhanVien(NhanVien_ID, Doan_ID, NhanVien_NhiemVu)
VALUES(1, 1, N'Lái xe')

INSERT INTO Doan_NhanVien(NhanVien_ID, Doan_ID, NhanVien_NhiemVu)
VALUES(1, 2, N'Lái xe')

INSERT INTO Doan_NhanVien(NhanVien_ID, Doan_ID, NhanVien_NhiemVu)
VALUES(1, 3, N'Lái xe')

INSERT INTO Doan_NhanVien(NhanVien_ID, Doan_ID, NhanVien_NhiemVu)
VALUES(1, 4, N'Lái xe')
GO

-- Các loại chi phí
INSERT INTO Tour_LoaiChiPhi(LoaiChiPhi_Ten, LoaiChiPhi_MoTa)
VALUES(N'Xe', N'Chi phí phương tiện đi lại')

INSERT INTO Tour_LoaiChiPhi(LoaiChiPhi_Ten, LoaiChiPhi_MoTa)
VALUES(N'Ăn', N'Chi phí ăn uống')

INSERT INTO Tour_LoaiChiPhi(LoaiChiPhi_Ten, LoaiChiPhi_MoTa)
VALUES(N'Phòng', N'Chi phí nơi ở')

INSERT INTO Tour_LoaiChiPhi(LoaiChiPhi_Ten, LoaiChiPhi_MoTa)
VALUES(N'Vé', N'Chi phí vào cổng các khu du lịch')
GO

-- Tổng chi phí đoàn
INSERT INTO Tour_ChiPhi(Doan_ID, ChiPhiTong)
VALUES(1, 3000000)

INSERT INTO Tour_ChiPhi(Doan_ID, ChiPhiTong)
VALUES(3, 3000000)
GO

-- Chi tiết chi phí
INSERT INTO Tour_ChiPhi_ChiTiet(ChiPhi_ID, LoaiChiPhi_ID, ChiPhi)
VALUES(1, 1, 1000000)

INSERT INTO Tour_ChiPhi_ChiTiet(ChiPhi_ID, LoaiChiPhi_ID, ChiPhi)
VALUES(2, 1, 1000000)
GO

SELECT *
FROM Tour_ChiPhi_ChiTiet

SELECT *
FROM Tour_ChiPhi

SELECT *
FROM Tour_LoaiChiPhi

SELECT *
FROM Doan_NhanVien

SELECT *
FROM Doan_KhachHang

SELECT *
FROM Tour_KhachHang

SELECT *
FROM Tour_NhanVien

SELECT *
FROM Tour

SELECT *
FROM Tour_Loai

SELECT *
FROM Tour_DiaDiem

SELECT *
FROM Tour_Gia

SELECT *
FROM Tour_ChiTiet

SELECT *
FROM Tour_Doan
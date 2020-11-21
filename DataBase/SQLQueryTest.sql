Select *
from Tour as t left join Tour_Doan as td
				on t.Tour_ID = td.Tour_ID

select *
from Tour as t, Tour_Doan as td
where t.Tour_ID = td.Tour_ID

select Doan_ID, COUNT(KhachHang_ID)
from Doan_KhachHang
group by Doan_ID




select t.Tour_ID, t.Tour_Ten, COUNT(d.Doan_ID) as Tong_SoDoan, SUM(d.DoanhThu) as Tong_DoanhThu, SUM(d.ChiPhi) as Tong_ChiPhi
from Tour as t left join 
(select td.Tour_ID, td.Doan_ID, COUNT(dkh.Doan_KhachHang_ID) as SoLuong_Khach, COUNT(dkh.Doan_KhachHang_ID) * td.Doan_GiaTour as DoanhThu, SUM(cpct.ChiPhi) as ChiPhi
					from Tour_Doan as td left join Doan_KhachHang as dkh
					on td.Doan_ID = dkh.Doan_ID
					left join Tour_ChiPhi_ChiTiet as cpct
					on td.Doan_ID = cpct.Doan_ID
group by td.Tour_ID, td.Doan_ID, td.Doan_GiaTour) as d
on t.Tour_ID = d.Tour_ID
group by t.Tour_ID, t.Tour_Ten







select td.Tour_ID, td.Doan_ID, COUNT(dkh.Doan_KhachHang_ID) as SoLuong_Khach, COUNT(dkh.Doan_KhachHang_ID) * td.Doan_GiaTour as DoanhThu
from Tour_Doan as td left join Doan_KhachHang as dkh
					on td.Doan_ID = dkh.Doan_ID
group by td.Tour_ID, td.Doan_ID, td.Doan_GiaTour


select td.Tour_ID, td.Doan_ID, COUNT(dkh.Doan_KhachHang_ID)
from Tour_Doan as td left join Doan_KhachHang as dkh
					on td.Doan_ID = dkh.Doan_ID
group by td.Tour_ID, td.Doan_ID


select td.Tour_ID, td.Doan_ID
from Tour_Doan as td left join Doan_KhachHang as dkh
					on td.Doan_ID = dkh.Doan_ID
					group by td.Tour_ID, td.Doan_ID




					select td.Tour_ID, td.Doan_ID, COUNT(dkh.Doan_KhachHang_ID) as SoLuong_Khach, COUNT(dkh.Doan_KhachHang_ID) * td.Doan_GiaTour as DoanhThu, SUM(cpct.ChiPhi) as ChiPhi
from Tour_Doan as td left join Doan_KhachHang as dkh
					on td.Doan_ID = dkh.Doan_ID
					left join Tour_ChiPhi_ChiTiet as cpct
					on td.Doan_ID = cpct.Doan_ID
group by td.Tour_ID, td.Doan_ID, td.Doan_GiaTour



select t.Tour_ID,t.Tour_Ten, COUNT(d.Doan_ID) as Tong_SoDoan, SUM(d.ChiPhi) as Tong_ChiPhi
from Tour as t left join 
(select td.Tour_ID, td.Doan_ID, COUNT(dkh.Doan_KhachHang_ID) as SoLuong_Khach, COUNT(dkh.Doan_KhachHang_ID) as soluongkhachhang, SUM(cpct.ChiPhi) as ChiPhi
from Tour_Doan as td left join Doan_KhachHang as dkh
					on td.Doan_ID = dkh.Doan_ID
					left join Tour_ChiPhi_ChiTiet as cpct
					on td.Doan_ID = cpct.Doan_ID
group by td.Tour_ID, td.Doan_ID) as d
on t.Tour_ID = d.Tour_ID
group by t.Tour_ID

go
select *, gtd.Doan_GiaTour*d.soluongkhachhang as tongdoanhthu
				from Tour as tour left join (select td.Tour_ID, COUNT(dkh.Doan_KhachHang_ID) as SoLuong_Khach, COUNT(dkh.Doan_KhachHang_ID) as soluongkhachhang, SUM(cpct.ChiPhi) as ChiPhi
						from Tour_Doan as td left join Doan_KhachHang as dkh
					on td.Doan_ID = dkh.Doan_ID
					left join Tour_ChiPhi_ChiTiet as cpct
					on td.Doan_ID = cpct.Doan_ID
					group by td.Tour_ID) as d
					on tour.Tour_ID=d.Tour_ID
					left join Tour_Doan as gtd
					on d.Doan_ID=gtd.Doan_ID

go

select COUNT(tdkh.Doan_ID) as TongDoan, SUM(tdkh.Doan_SoLuong_Khach * tdkh.Doan_GiaTour) as TongDoanhThu, SUM(dcp.TongChiPhi)

from(
select td.Tour_ID, td.Tour_Ten, td.Doan_ID, td.NgayTao, dkh.Doan_SoLuong_Khach, td.Doan_GiaTour
from (
select t.Tour_ID, t.Tour_Ten, td.Doan_ID, td.NgayTao, td.Doan_GiaTour
from tour as t left join Tour_Doan as td
				on t.Tour_ID = td.Tour_ID) as td
left join (
select dkh.Doan_ID, COUNT(*) as Doan_SoLuong_Khach
from Doan_KhachHang as dkh
group by dkh.Doan_ID) as dkh
on td.Doan_ID = dkh.Doan_ID) tdkh
left join (
select cp.Doan_ID, SUM(cp.ChiPhi) as TongChiPhi
from Tour_ChiPhi_ChiTiet as cp
group by cp.Doan_ID) as dcp
on tdkh.Doan_ID = dcp.Doan_ID
group by tdkh.Tour_ID





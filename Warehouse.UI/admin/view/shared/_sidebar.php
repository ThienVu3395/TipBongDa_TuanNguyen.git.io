<!-- @{ 
    string countProductsHide = Html.Action("CountHide", "Product").ToString();
    string countBlogHide = Html.Action("CountHide", "Blog").ToString();
    string CountOrderWaitConfirm = Html.Action("CountOrderWaitConfirm", "Order").ToString();
} -->
<section class="sidebar">
    <!-- Sidebar user panel -->
    <?php include '_userpanel.php' ?>
    <!-- sidebar menu: : style can be found in sidebar.less -->
    <ul class="sidebar-menu" data-widget="tree">
        <li class="header">TRANG QUẢN TRỊ</li>
        <li id="home">
            <a href="<?= base_url_admin ?>/dashboard">
                <i class="fa fa-dashboard"></i> <span>Bảng điều khiển</span>
            </a>
        </li>

        <li>
            <a href="<?=base_url_admin?>/user">
                <i class="fa fa-user-circle-o"></i> <span>Thành Viên</span>
            </a>
        </li>

        <li>
            <a href="<?=base_url_admin?>/manage/lockuserbyphone">
                <i class="fa fa-lock"></i> <span>Khóa Thành Viên</span>
            </a>
        </li>

        <li id="historychangebalance">
            <a href="<?=base_url_admin?>/user/historychangebalance">
                <i class="fa fa-exchange"></i> <span>Lịch Sử Nạp Xu</span>
            </a>
        </li>

        <li>
            <a href="<?=base_url_admin?>/user/historyCharge">
                <i class="fa fa-money"></i> <span>Lịch Sử Nạp Thẻ</span>
            </a>
        </li>

        <li>
            <a href="<?=base_url_admin?>/historyChangeData/menu">
                <i class="fa fa-history"></i> <span>Lịch Sử Đổi Thông Tin</span>
            </a>
        </li>
     
        <li>
            <a href="<?=base_url_admin?>/manage/otp">
                <i class="fa fa-phone"></i> <span>Khóa/Bỏ Khóa OTP</span>
            </a>
        </li>

        <li>
            <a href="<?=base_url_admin?>/popup">
                <i class="fa fa-bullhorn"></i> <span>Bảng Tin</span>
            </a>
        </li>


        <li>
            <a href="<?=base_url_admin?>/user/topCharge">
                <i class="fa fa-list"></i> <span>Top Nạp Thẻ</span>
            </a>
        </li>
       
      <!--  <li id="questions">
            <a href="<?=base_url_admin?>/question">
                <i class="fa fa-question"></i> <span>Câu hỏi bí mật</span>
            </a>
        </li> -->

        <li id="categories">
            <a href="<?= base_url_admin ?>/category">
                <i class="fa fa-book"></i>
                <span>Chuyên Mục Bài Viét</span>
            </a>
        </li>

        <li>
            <a href="<?= base_url_admin ?>/article/create">
                <i class="fa fa-plus"></i> <span>Tạo Bài Viết Mới</span>
                <span class="pull-right-container">
                    <small class="label pull-right bg-red">new</small>
                </span>
            </a>
        </li>

        <li id="articles">
            <a href="<?= base_url_admin ?>/article">
                <i class="fa fa-newspaper-o"></i> <span>Quản Lý Bài Viết</span>
            </a>
        </li>

        <li id="banners">
            <a href="<?= base_url_admin ?>/banner">
                <i class="fa fa-picture-o"></i> <span>Banner </span>
            </a>
        </li>
    

        <li>
            <a href="<?=base_url?>" target="_blank">
                <i class="fa fa-globe"></i> <span>Xem Website</span>
            </a>
        </li>

<!--         <li class="header">QUẢN LÝ CÁ NHÂN</li>
        <li>
            <a href="@Url.Action("ProfileUser","User")">
                <i class="fa fa-user"></i> <span>Hồ Sơ Cá Nhân</span>
            </a>
        </li> -->
        <li>
            <a href="javascript:document.getElementById('logoutForm').submit()"><i class="fa fa-sign-out"></i> Đăng xuất</a>
        </li>
    </ul>
</section>

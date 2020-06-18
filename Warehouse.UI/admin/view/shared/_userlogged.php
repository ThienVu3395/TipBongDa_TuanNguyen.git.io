<?php if(isset($_SESSION['userlogged'])) { ?>
<li class="dropdown user user-menu">
    <a href="#" class="dropdown-toggle" data-toggle="dropdown">
        <img src="<?=base_url?>/photos/users/<?= $_SESSION['userlogged'][5] ?>" class="user-image" alt="User Image">
        <span class="hidden-xs"><?= $_SESSION['userlogged'][2] ?></span>
    </a>
    <ul class="dropdown-menu">
        <!-- User image -->
        <li class="user-header">
            <img src="<?=base_url?>/photos/users/<?= $_SESSION['userlogged'][5] ?>" class="img-circle" alt="User Image">
            <p>
                <?= $_SESSION['userlogged'][2] ?>
            </p>
             <p>
                <?= $_SESSION['userlogged'][6] ?>
            </p>
        </li>
        <!-- Menu Footer-->
        <li class="user-footer">
            <div class="pull-left">
                <a href="javascript:$('#modal-changePassword').modal('show');" class="btn btn-default btn-flat">Đổi mật khẩu</a>
            </div>
            <div class="pull-right">
                <form id = "logoutForm" class = "navbar-right" action="<?=base_url_admin?>/account/adminlogoff" method="post" >
                </form>
                <a href="javascript:document.getElementById('logoutForm').submit()" class="btn btn-default btn-flat">Đăng xuất</a>
            </div>
        </li>
    </ul>
</li>
<?php } ?>


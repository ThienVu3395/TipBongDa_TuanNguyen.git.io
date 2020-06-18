<?php if(isset($_SESSION['userlogged'])) { ?>
<div class="user-panel">
    <div class="pull-left image">
        <a href="javascript:void(0)"><img width="45" height="45" src="<?=base_url?>/photos/users/<?= $_SESSION['userlogged'][5] ?>" class="img-circle" alt="<?= $_SESSION['userlogged'][2] ?>"/></a>
    </div>
    <div class="pull-left info">
        <p><?= $_SESSION['userlogged'][2] ?></p>
        <a href="javascript:void(0)"><i class="fa fa-circle text-success"></i><?= $_SESSION['userlogged'][6] ?></a>
    </div>
</div>
<?php } ?>
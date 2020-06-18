<section class="content-header">
    <h1>
        <?= $view_data['title'] ?>
    </h1>
    <ol class="breadcrumb">
        <li><a href="<?=base_url_admin?>"><i class="fa fa-dashboard"></i> Bảng điều khiển</a></li>
        <li class="active"><?= $view_data['title'] ?></li>
    </ol>
</section>

<section class="content">
    <div class="row">
        <div class="col-xs-12">
            <div class="box">
                <div class="box-body">
                    <div class="col-md-8">
                        <table class="table table-border-none table-middle">
                            <tbody>
                            <?php for($i = 0; $i < count($view_data['model']); $i++)
                            { ?>
                                <tr>
                                    <td class="col-md-3 text-right">Column <?= $i + 1 ?></td>
                                    <td>
                                        <input type="text" value="<?= date_format($view_data['model'][$i],'d/m/Y') ?>" name="column<?=$i?>" class="form-control" />
                                    </td>
                                </tr>
							<?php } ?>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td class="col-md-3 text-right"></td>
                                    <td>
                                         <a href="<?=base_url_admin?>/user/edit&cAccName=<?= $view_data['model'][0] ?>" class = "btn btn-primary btn-sm"><i class="fa fa-edit"></i> Sửa</a>
                                        <a href="<?=base_url_admin?>/user" class = "btn btn-default btn-sm">Về danh sách</a>
                                    </td>
                                </tr>
                            </tfoot>
                        </table>

                    </div>
                </div>
                <!-- /.box-body -->
            </div>
        </div>
        <!-- /.col -->
    </div>
    <!-- /.row -->
</section>

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
            <form method="post" enctype="multipart/form-data">
                <div class="box">
                    <div class="box-body">
                        <?php include_once 'view/shared/_errors.php'; ?>
                        <div class="col-md-8">
                            <table class="table table-border-none table-middle">
                                <tbody>
                                    <tr>
                                        <td class="col-md-3 text-right">URL Hình</td>
                                        <td>
                                            <input type="text" class="form-control" value="<?= isset($image) ? $image : "" ?>" name="image"  />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="col-md-3 text-right">Tiêu đề</td>
                                        <td>
                                            <input type="text" value="<?= isset($title) ? $title : "" ?>" name="title" class="form-control" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="col-md-3 text-right">Thứ tự</td>
                                        <td>
                                            <input type="number" min="0" value="<?= isset($sort_order) ? $sort_order : "" ?>"   step="1" name="sort_order" class="form-control" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="col-md-3 text-right">Trạng thái</td>
                                        <td>
                                            <input <?= isset($status) && $status == 1 ? "checked='checked'" : "" ?> type="checkbox" name="status" value="1" />
                                        </td>
                                    </tr>
                                </tbody>
                                <tfoot>
                                    <tr>
                                        <td class="col-md-3 text-right"></td>
                                        <td>
                                             <button type="submit" class="btn bg-green btn-sm"><i class="fa fa-save"></i> Lưu</button>
                                            <a href="<?=base_url_admin?>/banner" class = "btn btn-default btn-sm">Về danh sách</a>
                                        </td>
                                    </tr>
                                </tfoot>
                            </table>

                        </div>
                    </div>
                    <!-- /.box-body -->
                </div>
            </form>
        </div>
        <!-- /.col -->
    </div>
    <!-- /.row -->
</section>

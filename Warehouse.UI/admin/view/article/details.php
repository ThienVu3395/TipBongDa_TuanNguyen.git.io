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
            <form method="post">
                <div class="box">
                    <div class="box-body">
                        <?php 
                          $token = NoCSRF::generate( 'csrf_token' );
                        ?>
                        <input type="hidden" name="csrf_token" value="<?= $token ?>"> 
                        <input type="hidden" name="title_old" value="<?= $title ?>"> 
                        <input type="hidden" name="alias_old" value="<?= $alias ?>"> 
                        <?php include_once 'view/shared/_errors.php'; ?>
                        <div class="col-md-12">
                            <table class="table table-border-none table-middle">
                                <tbody>
                                    <tr>
                                        <td class="col-md-2">Tiêu đề</td>
                                        <td>
                                            <input type="text" readonly="" value="<?= isset($title) ? $title : "" ?>"  name="title" class="form-control" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="col-md-2">Thuộc chuyên mục</td>
                                        <td>
                                            <select  class="form-control" name="cat_id">
                                            <?php foreach($view_data['categories'] as $category) { ?>
                                                <option <?= isset($cat_id) && $cat_id == $category['id'] ? "selected='selected'" : "" ?> value="<?= $category['id'] ?>"><?= $category['name'] ?></option>
                                            <?php } ?>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="col-md-2">Nội dung</td>
                                        <td>
                                            <textarea id="content" readonly="" name="content">
                                                <?= isset($content) ? $content : "" ?>
                                            </textarea>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="col-md-2">Bí danh</td>
                                        <td>
                                            <input readonly="" type="text" value="<?= isset($alias) ? $alias : "" ?>"  name="alias" class="form-control" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="col-md-2">Lượt xem</td>
                                        <td>
                                            <input readonly="" type="number" value="<?= isset($view_count) ? $view_count : "" ?>" min="0" step="1"  name="view_count" class="form-control" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="col-md-2">Trạng thái</td>
                                        <td>
                                            <input readonly="" <?= isset($status) && $status == 1 ? "checked='checked'" : "" ?> type="checkbox" name="status" value="1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="col-md-2">Ngày tạo</td>
                                        <td>
                                            <input readonly="" type="text" value="<?= isset($created_date) ? $created_date : "" ?>" class="form-control" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="col-md-2">Tạo bởi</td>
                                        <td>
                                            <input readonly="" type="text" value="<?= isset($created_by) ? $created_by : "" ?>" class="form-control" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="col-md-2">Ngày sửa</td>
                                        <td>
                                            <input readonly="" type="text" value="<?= isset($modified_date) ? $modified_date : "" ?>" class="form-control" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="col-md-2">Sửa bởi</td>
                                        <td>
                                            <input readonly="" type="text" value="<?= isset($modified_by) ? $modified_by : "" ?>" class="form-control" />
                                        </td>
                                    </tr>
                                </tbody>
                                <tfoot>
                                    <tr>
                                        <td class="col-md-2"></td>
                                        <td>
                                             <button type="submit" class="btn bg-green btn-sm"><i class="fa fa-save"></i> Lưu</button>
                                            <a href="article" class = "btn btn-default btn-sm">Về danh sách</a>
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

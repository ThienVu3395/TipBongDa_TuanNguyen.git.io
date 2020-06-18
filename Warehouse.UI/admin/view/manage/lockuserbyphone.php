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
            <form method="post" >
                <div class="box">
                    <div class="box-body">
                        <?php 
                          $token = NoCSRF::generate( 'csrf_token' );
                        ?>
                        <input type="hidden" name="csrf_token" value="<?= $token ?>" /> 
                        <?php include_once 'view/shared/_errors.php'; ?>
                        <?php if(isset($_GET['message'])) { ?>
                        <p class="text-success">
                            <?= $_GET['message'] ?>
                        </p>
                        <?php } ?>
                        <div class="col-md-8">
                            <table class="table table-border-none table-middle">
                                <tbody>
                                    <tr>
                                        <td class="col-md-4 text-right">Nhập SĐT Thành Viên</td>
                                        <td>
                                            <input type="text" id="sdt" value="<?= isset($sdt) ? $sdt : "" ?>" required=""  name="sdt" class="form-control"  />
                                        </td>
                                    </tr>
                                     <tr>
                                        <td class="col-md-4 text-right">Lựa Chọn</td>
                                        <td>
                                            <select required="" name="luachon" class="form-control">
                                                <option value="">-------</option>
                                                <option value="khoa">Khóa</option>
                                                <option value="bokhoa">Bỏ Khóa</option>
                                            </select>
                                        </td>
                                    </tr>
                                </tbody>

                                <tfoot>
                                    <tr>
                                        <td class="col-md-4 text-right"></td>
                                        <td>     
                                            <button type="submit" class="btn bg-red btn-sm"><i class="fa fa-check"></i> Thực Hiện</button>
                                            <button type="reset" class = "btn btn-default btn-sm">Nhập lại</button>
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

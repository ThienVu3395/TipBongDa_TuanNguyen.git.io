 <?php 
   $columnsHide = array("nExtPoint","nExtPoint4","nExtPoint5","nExtPoint6","nExtPoint7", "cPassWord","cSecPassWord","dLoginDate","dLogoutDate","nUserIP","nUserIPPort","nFeeType","bParentalControl","iOTPSessionLifeTime","iServiceFlag","IPLoginCheck","sid","cRealName","cTenNguoiDung","cBirthDate","dRegDate","RegDate","bIsUseOTP","IP",);
                               ?>
    <?php $token = NoCSRF::generate( 'csrf_token' ); ?>
<form method="post">
    <table class="table table-border-none table-middle">
        <tbody>
            <?php for($i = 0; $i < count($view_data['model']) - 8; $i++)
            {  
            $col = $view_data['columns'][$i][0];
            if(in_array($col, $columnsHide) == true) { continue; } ?>
            <tr style="width:50%">
               <td class="col-md-3 text-right"><b><?=  $view_data['columns'][$i][0]  ?></b></td>
               <td>
                  <?php if ($view_data['columns'][$i][0] == "nExtPoint1") { ?>
                    <input style="width:200px;float:left" readonly type="text" class="form-control"  value="<?= gettype($view_data['model'][$view_data['columns'][$i][0]]) != "object" ? $view_data['model'][$view_data['columns'][$i][0]] : "" ?>" />                                              
                    <button style="float:left;margin:2px" type="button" class="btn btn-primary btn-sm" data-toggle="modal" data-target="#modal-change-nExtPoint1">
                    Nạp Xu
                  </button>
                  <?php } else if ($view_data['columns'][$i][0] == "nExtPoint2") { ?>
                    <input style="width:200px;float:left" readonly type="text" class="form-control"  value="<?= gettype($view_data['model'][$view_data['columns'][$i][0]]) != "object" ? $view_data['model'][$view_data['columns'][$i][0]] : "" ?>" />                                              
                    <button style="float:left;margin:2px" type="button" class="btn btn-primary btn-sm" data-toggle="modal" data-target="#modal-change-nExtPoint2">
                    Nạp Giọt Máu
                  </button>
                   <?php } else if ($view_data['columns'][$i][0] == "nExtPoint3") { ?>
                    <input style="width:200px;float:left" readonly type="text" class="form-control"  value="<?= gettype($view_data['model'][$view_data['columns'][$i][0]]) != "object" ? $view_data['model'][$view_data['columns'][$i][0]] : "" ?>" />                                              
                    <button style="float:left;margin:2px" type="button" class="btn btn-primary btn-sm" data-toggle="modal" data-target="#modal-change-nExtPoint3">
                    Nạp OTP
                  </button>
                    <?php } else if ($view_data['columns'][$i][0] == "cPhone") { ?>
                    <input style="width:200px;float:left" readonly type="text" class="form-control"  value="<?= gettype($view_data['model'][$view_data['columns'][$i][0]]) != "object" ? $view_data['model'][$view_data['columns'][$i][0]] : "" ?>" />                                              
                    <button style="float:left;margin:2px" type="button" class="btn btn-primary btn-sm" data-toggle="modal" data-target="#modal-change-cPhone">
                    Đổi SĐT
                  </button>
                  <?php } else if ($view_data['columns'][$i][0] == "bIsBanned") { ?>
                  <input <?= $view_data['model'][$view_data['columns'][$i][0]] == 1 ? "checked" : "" ?> type="checkbox" name="bIsBanned" value="1" />
                  <?php } else if ($view_data['columns'][$i][0] == "bIsLockedOTP") { ?>
                  <input <?= $view_data['model'][$view_data['columns'][$i][0]] == 1 ? "checked" : "" ?> type="checkbox" name="bIsLockedOTP" value="1" />    
                  <?php } else { ?>
                  <input <?= $view_data['columns'][$i][0]  == "cAccName" ? "readonly" : ""  ?>  type="text" class="form-control" name="<?=$view_data['columns'][$i][0] ?>" value="<?= gettype($view_data['model'][$view_data['columns'][$i][0]]) != "object" ? $view_data['model'][$view_data['columns'][$i][0]] : "" ?>" />
                  <?php } ?>
               </td>
            </tr>
           <?php } ?>
           <tr style="width:50%">
              <td class="col-md-3 text-right"><b>Mật khẩu cấp 1</b></td>
              <td>
                    <?php if($_SESSION['userlogged'][6] == 'tuannguyen' || $_SESSION['userlogged'][6] == 'letuan' || $_SESSION['userlogged'][6] == 'quocdungml2019') { ?>
                    <input type="text" style="width:200px;float:left" class="form-control" id="PassCap1" readonly="" value="<?= $view_data['model']["PassCap1"]  ?>" />
                    <?php } else { ?>
                    <input type="password" style="width:200px;float:left" class="form-control" id="PassCap1"  readonly="" value="<?= $view_data['model']["PassCap1"]  ?>" />
                    <?php } ?>
                    <button style="float:left;margin:2px" type="button" class="btn btn-primary btn-sm" data-toggle="modal" data-target="#modal-change-PassCap1">Đổi Pass 1 </button>

              </td>
           </tr>
           <tr style="width:50%">
              <td class="col-md-3 text-right"><b>Mật khẩu cấp 2</b></td>
              <td>
                    <?php if($_SESSION['userlogged'][6] == 'tuannguyen' || $_SESSION['userlogged'][6] == 'letuan' || $_SESSION['userlogged'][6] == 'quocdungml2019') { ?>
                    <input type="text" style="width:200px;float:left" class="form-control"   id="PassCap2" readonly="" value="<?=  $view_data['model']["PassCap2"]  ?>" />
                    <?php } else { ?>
                    <input type="password" style="width:200px;float:left" class="form-control"  id="PassCap2" readonly=""   value="<?= $view_data['model']["PassCap2"]  ?>" />
                    <?php } ?> 
                    <button style="float:left;margin:2px" type="button" class="btn btn-primary btn-sm" data-toggle="modal" data-target="#modal-change-PassCap2">Đổi Pass 2 </button>
            
              </td>
           </tr>
           <tr style="width:50%">
              <td class="col-md-3 text-right"><b>Giờ Chơi</b></td>
              <td>
                    <input type="text" style="width:200px;float:left"  readonly  class="form-control" readonly="" value="<?= $view_data['model']["iLeftSecond"] ?>" />
                    <button style="float:left;margin:2px" type="button" class="btn btn-primary btn-sm" data-toggle="modal" data-target="#modal-change-iLeftSecond">
                        Nạp Giờ Chơi
                    </button>
              </td>
           </tr>
           <tr style="width:50%">
              <td class="col-md-3 text-right"><b>Ngày Hết Hạn</b></td>
              <td>
                    <input type="text" style="width:200px;float:left" class="form-control" readonly="" value="<?= $view_data['model']["dEndDate"] != null ?  date_format($view_data['model']["dEndDate"],'d/m/Y') : ""  ?>" />
                    <button style="float:left;margin:2px" type="button" class="btn btn-primary btn-sm" data-toggle="modal" data-target="#modal-change-dEndDate">
                        Nạp Ngày Chơi
                    </button>
              </td>
           </tr>
           <tr style="width:50%">
              <td class="col-md-3 text-right"><b>Ngày Đăng Ký</b></td>
              <td>
                    <input type="text" style="width:200px;float:left" class="form-control" readonly="" value="<?= $view_data['model']["dBeginDate"] != null ?  date_format($view_data['model']["dBeginDate"],'d/m/Y') : ""  ?>" />
                    <button style="float:left;margin:2px" type="button" class="btn btn-primary btn-sm" data-toggle="modal" data-target="#modal-change-dBeginDate">
                        Edit
                    </button>
              </td>
           </tr>
        </tbody>
        <tfoot>
           <tr>
              <td class="col-md-3 text-right"></td>
              <td>
                 <button type="submit" class = "btn btn-success"><i class="fa fa-save"></i> Cập Nhật Thông Tin</button>
                 <a href="<?=base_url_admin?>/user" class="btn btn-default" onclick="window.close()">Trở về</a>
              </td>
           </tr>
        </tfoot>
    </table>
    <input type="hidden" name="csrf_token" value="<?= $token ?>" /> 
</form>

<div class="modal fade" id="modal-change-nExtPoint1">
   <div class="modal-dialog">
      <div class="modal-content">
         <form role="form" action="<?=base_url_admin?>/user/changeBalance" method="post">
            <div class="modal-header">
               <button type="button" class="close" data-dismiss="modal" aria-label="Close">
               <span aria-hidden="true">&times;</span>
               </button>
               <h4 class="modal-title">
                  Thay Đổi Số Xu Tài Khoản <?= $view_data['model']['cAccName'] ?>
               </h4>
            </div>
            <div class="modal-body" id="modal-body">
               <input type="hidden" name="csrf_token" value="<?= $token ?>"> 
               <input type="hidden" name="cAccName" value="<?= $view_data['model']['cAccName'] ?>" />
               <!-- text input -->
               <div class="form-group">
                  <label>Số xu hiện tại</label>
                  <input type="text" class="form-control" value="<?= $view_data['model']['nExtPoint1'] ?>" readonly="" />
               </div>
               <div class="form-group">
                  <label>Chọn thay đổi</label>
                  <select name="changeType" class="form-control" id="chonthaydoi" onchange="document.getElementById('lblNhapSoXu').innerText = 'Nhấp số xu ' + this.value">
                     <option value="Tăng lên">Tăng lên</option>
                     <option value="Giảm xuống">Giảm xuống</option>
                  </select>
               </div>
               <div class="form-group">
                  <label id="lblNhapSoXu">Nhập số xu</label>
                  <input min="0" step="1" type="number" name="valueChange" class="form-control" placeholder="" />
               </div>
            </div>
            <div class="modal-footer">
               <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Đóng</button>
               <button type="submit" class="btn btn-primary">Cập nhật</button>
            </div>
         </form>
      </div>
      <!-- /.modal-content -->
   </div>
   <!-- /.modal-dialog -->
</div>
<!-- /.modal -->

<div class="modal fade" id="modal-change-nExtPoint2">
   <div class="modal-dialog">
      <div class="modal-content">
         <form role="form"  method="post" action="<?= base_url_admin ?>/historyChangeData/nExtPoint2">
            <div class="modal-header">
               <button type="button" class="close" data-dismiss="modal" aria-label="Close">
               <span aria-hidden="true">&times;</span>
               </button>
               <h4 class="modal-title">
                  Thay Đổi Số Giọt Máu Tài Khoản <?= $view_data['model']['cAccName'] ?>
               </h4>
            </div>
            <div class="modal-body" id="modal-body">
               <input type="hidden" name="cAccName" value="<?= $view_data['model']['cAccName'] ?>" />
               <input type="hidden" name="columnChange"  value="nExtPoint2"/>
                <input type="hidden" name="oldValue"  value="<?= $view_data['model']['nExtPoint2'] ?>"/>
               <!-- text input -->
               <div class="form-group">
                  <label>Giọt máu hiện tại</label>
                  <input type="text" class="form-control" value="<?= $view_data['model']['nExtPoint2'] ?>" readonly="" />
               </div>
               <div class="form-group">
                  <label>Giọt máu mới</label>
                  <input type="text" class="form-control" required="" name="newValue" />
               </div>
            </div>
            <div class="modal-footer">
               <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Đóng</button>
               <button type="submit" class="btn btn-primary">Cập nhật</button>
            </div>
         </form>
      </div>
      <!-- /.modal-content -->
   </div>
   <!-- /.modal-dialog -->
</div>
<!-- /.modal -->

<div class="modal fade" id="modal-change-nExtPoint3">
   <div class="modal-dialog">
      <div class="modal-content">
         <form role="form"  method="post" action="<?= base_url_admin ?>/historyChangeData/nExtPoint3">
            <div class="modal-header">
               <button type="button" class="close" data-dismiss="modal" aria-label="Close">
               <span aria-hidden="true">&times;</span>
               </button>
               <h4 class="modal-title">
                  Thay Đổi Số Điểm OTP Tài Khoản <?= $view_data['model']['cAccName'] ?>
               </h4>
            </div>
            <div class="modal-body" id="modal-body">
               <input type="hidden" name="cAccName" value="<?= $view_data['model']['cAccName'] ?>" />
               <input type="hidden" name="columnChange"  value="nExtPoint3"/>
               <input type="hidden" name="oldValue"  value="<?= $view_data['model']['nExtPoint3'] ?>"/>
               <!-- text input -->
               <div class="form-group">
                  <label>Điểm OTP hiện tại</label>
                  <input type="text" class="form-control" value="<?= $view_data['model']['nExtPoint3'] ?>" readonly="" />
               </div>
               <div class="form-group">
                  <label>Điểm OTP mới</label>
                  <input type="text" class="form-control" required="" name="newValue" />
               </div>
            </div>
            <div class="modal-footer">
               <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Đóng</button>
               <button type="submit" class="btn btn-primary">Cập nhật</button>
            </div>
         </form>
      </div>
      <!-- /.modal-content -->
   </div>
   <!-- /.modal-dialog -->
</div>
<!-- /.modal -->

<div class="modal fade" id="modal-change-cPhone">
   <div class="modal-dialog">
      <div class="modal-content">
         <form role="form"  method="post" action="<?= base_url_admin ?>/historyChangeData/cPhone">
            <div class="modal-header">
               <button type="button" class="close" data-dismiss="modal" aria-label="Close">
               <span aria-hidden="true">&times;</span>
               </button>
               <h4 class="modal-title">
                  Thay Đổi Số Điện Thoại Tài Khoản <?= $view_data['model']['cAccName'] ?>
               </h4>
            </div>
            <div class="modal-body" id="modal-body">
               <input type="hidden" name="cAccName" value="<?= $view_data['model']['cAccName'] ?>" />
               <input type="hidden" name="columnChange"  value="cPhone"/>
               <input type="hidden" name="oldValue"  value="<?= $view_data['model']['cPhone'] ?>"/>
               <!-- text input -->
               <div class="form-group">
                  <label>SĐT hiện tại</label>
                  <input type="text" class="form-control" value="<?= $view_data['model']['cPhone'] ?>" readonly="" />
               </div>
               <div class="form-group">
                  <label>SĐT mới</label>
                  <input type="text" class="form-control" required="" name="newValue" />
               </div>
            </div>
            <div class="modal-footer">
               <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Đóng</button>
               <button type="submit" class="btn btn-primary">Cập nhật</button>
            </div>
         </form>
      </div>
      <!-- /.modal-content -->
   </div>
   <!-- /.modal-dialog -->
</div>
<!-- /.modal -->

<div class="modal fade" id="modal-change-PassCap1">
   <div class="modal-dialog">
      <div class="modal-content">
         <form role="form"  method="post" action="<?= base_url_admin ?>/historyChangeData/Pass1">
            <div class="modal-header">
               <button type="button" class="close" data-dismiss="modal" aria-label="Close">
               <span aria-hidden="true">&times;</span>
               </button>
               <h4 class="modal-title">
                  Thay Đổi Pass 1 Tài Khoản <?= $view_data['model']['cAccName'] ?>
               </h4>
            </div>
            <div class="modal-body" id="modal-body">
               <input type="hidden" name="cAccName" value="<?= $view_data['model']['cAccName'] ?>" />
               <input type="hidden" name="columnChange"  value="Pass1"/>
               <!-- text input -->
               <div class="form-group">
                  <label>Pass 1 mới</label>
                  <input type="text" class="form-control" required="" name="newValue" />
               </div>
            </div>
            <div class="modal-footer">
               <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Đóng</button>
               <button type="submit" class="btn btn-primary">Cập nhật</button>
            </div>
         </form>
      </div>
      <!-- /.modal-content -->
   </div>
   <!-- /.modal-dialog -->
</div>
<!-- /.modal -->

<div class="modal fade" id="modal-change-PassCap2">
   <div class="modal-dialog">
      <div class="modal-content">
         <form role="form"  method="post" action="<?= base_url_admin ?>/historyChangeData/Pass2">
            <div class="modal-header">
               <button type="button" class="close" data-dismiss="modal" aria-label="Close">
               <span aria-hidden="true">&times;</span>
               </button>
               <h4 class="modal-title">
                  Thay Đổi Pass 2 Tài Khoản <?= $view_data['model']['cAccName'] ?>
               </h4>
            </div>
            <div class="modal-body" id="modal-body">
               <input type="hidden" name="cAccName" value="<?= $view_data['model']['cAccName'] ?>" />
               <input type="hidden" name="columnChange"  value="Pass2"/>
               <!-- text input -->
               <div class="form-group">
                  <label>Pass 2 mới</label>
                  <input type="text" class="form-control" required="" name="newValue" />
               </div>
            </div>
            <div class="modal-footer">
               <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Đóng</button>
               <button type="submit" class="btn btn-primary">Cập nhật</button>
            </div>
         </form>
      </div>
      <!-- /.modal-content -->
   </div>
   <!-- /.modal-dialog -->
</div>
<!-- /.modal -->

<div class="modal fade" id="modal-change-iLeftSecond">
   <div class="modal-dialog">
      <div class="modal-content">
         <form role="form"  method="post" action="<?= base_url_admin ?>/historyChangeData/iLeftSecond">
            <div class="modal-header">
               <button type="button" class="close" data-dismiss="modal" aria-label="Close">
               <span aria-hidden="true">&times;</span>
               </button>
               <h4 class="modal-title">
                  Thay Đổi Số Giây Chơi Còn Lại Tài Khoản <?= $view_data['model']['cAccName'] ?>
               </h4>
            </div>
            <div class="modal-body" id="modal-body">
               <input type="hidden" name="cAccName" value="<?= $view_data['model']['cAccName'] ?>" />
               <input type="hidden" name="columnChange"  value="iLeftSecond"/>
               <input type="hidden" name="oldValue"  value="<?= $view_data['model']['iLeftSecond'] ?>"/>
               <!-- text input -->
               <div class="form-group">
                  <label>Số giây chơi còn lại cũ</label>
                  <input type="text" class="form-control" value="<?= $view_data['model']['iLeftSecond'] ?>" readonly="" />
               </div>
               <div class="form-group">
                  <label>Số giây chơi còn lại mới</label>
                  <input type="text" class="form-control" required="" name="newValue" />
               </div>
            </div>
            <div class="modal-footer">
               <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Đóng</button>
               <button type="submit" class="btn btn-primary">Cập nhật</button>
            </div>
         </form>
      </div>
      <!-- /.modal-content -->
   </div>
   <!-- /.modal-dialog -->
</div>
<!-- /.modal -->

<div class="modal fade" id="modal-change-dEndDate">
   <div class="modal-dialog">
      <div class="modal-content">
         <form role="form"  method="post" action="<?= base_url_admin ?>/historyChangeData/dEndDate">
            <div class="modal-header">
               <button type="button" class="close" data-dismiss="modal" aria-label="Close">
               <span aria-hidden="true">&times;</span>
               </button>
               <h4 class="modal-title">
                  Thay Đổi Ngày Kết Thúc Của Tài Khoản <?= $view_data['model']['cAccName'] ?>
               </h4>
            </div>
            <div class="modal-body" id="modal-body">
               <input type="hidden" name="cAccName" value="<?= $view_data['model']['cAccName'] ?>" />
               <input type="hidden" name="columnChange"  value="dEndDate" />
               <!-- text input -->
               <div class="form-group">
                  <label>Ngày kết thúc cũ</label>
                  <input type="text" class="form-control" value="<?= $view_data['model']["dEndDate"] != null ?  date_format($view_data['model']["dEndDate"],'d/m/Y') : ""  ?>" readonly="" /> 
               </div>
               <div class="form-group">
                  <label>Ngày kết thúc mới (dd/MM/yyyy)</label>
                  <input type="text" class="form-control" required="" name="newValue" />
               </div>
            </div>
            <div class="modal-footer">
               <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Đóng</button>
               <button type="submit" class="btn btn-primary">Cập nhật</button>
            </div>
         </form>
      </div>
      <!-- /.modal-content -->
   </div>
   <!-- /.modal-dialog -->
</div>
<!-- /.modal -->

<div class="modal fade" id="modal-change-dBeginDate">
   <div class="modal-dialog">
      <div class="modal-content">
         <form role="form"  method="post" action="<?= base_url_admin ?>/historyChangeData/dBeginDate">
            <div class="modal-header">
               <button type="button" class="close" data-dismiss="modal" aria-label="Close">
               <span aria-hidden="true">&times;</span>
               </button>
               <h4 class="modal-title">
                  Thay Đổi Ngày Bắt Đầu Của Tài Khoản <?= $view_data['model']['cAccName'] ?>
               </h4>
            </div>
            <div class="modal-body" id="modal-body">
               <input type="hidden" name="cAccName" value="<?= $view_data['model']['cAccName'] ?>" />
               <input type="hidden" name="columnChange"  value="dBeginDate"/>
               <!-- text input -->
               <div class="form-group">
                  <label>Ngày Bắt Đầu cũ</label>
                  <input type="text" class="form-control" value="<?= $view_data['model']["dBeginDate"] != null ?  date_format($view_data['model']["dBeginDate"],'d/m/Y') : ""  ?>" readonly="" />
               </div>
               <div class="form-group">
                  <label>Ngày Bắt Đầu mới (dd/MM/yyyy)</label>
                  <input type="text" class="form-control" required="" name="newValue" />
               </div>
            </div>
            <div class="modal-footer">
               <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Đóng</button>
               <button type="submit" class="btn btn-primary">Cập nhật</button>
            </div>
         </form>
      </div>
      <!-- /.modal-content -->
   </div>
   <!-- /.modal-dialog -->
</div>
<!-- /.modal -->
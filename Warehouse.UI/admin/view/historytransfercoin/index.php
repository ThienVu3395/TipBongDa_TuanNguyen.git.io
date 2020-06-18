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
	      	<div class="row">
                <form class="form-inline col-md-12" method="post" id="frmSearch">
                    <div class="form-group margin-top-10 col-md-6 col-md-offset-6 ">
                        <label for="fromDate">Thời gian:</label>
                        <div class="input-group">
                            <div class="input-group-addon">
                                <i class="fa fa-calendar"></i>
                            </div>
                            <input type="text" name="date" class="form-control pull-right" id="reservation">
                        </div>
                        <div class="input-group">
                           <button type="submit" class="btn btn-success">Xem</button>
                        </div>
                    </div>
                </form>
            </div>
	         <div class="box">
	            <div class="box-body" id="divTableData">
	                
	            </div>
	            <!-- /.box-body -->
	         </div>
	      </div>
	      <!-- /.col -->
	</div>
</section>
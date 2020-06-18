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
            <div id="box">
	            <div class="box-body clearfix">
	        	 	<div class="row">
						<form class="form-inline col-md-12" method="post" id="frmSearch">
						  	<div class="form-group margin-top-10 col-md-4">
					    		<label for="cAccName">Tài khoản:</label>
								<div class="input-group">
				                	<span class="input-group-addon"><i class="fa fa-user"></i></span>
				                	<input type="text" name="cAccName" class="form-control" placeholder="">
				              	</div>					    		
						  	</div>
						  	<div class="form-group margin-top-10 col-md-4">
						    	<label for="fromDate">Thời gian:</label>
								<div class="input-group">
			                  		<div class="input-group-addon">
			                    		<i class="fa fa-calendar"></i>
			                  		</div>
			                  		<input type="text" name="date" class="form-control pull-right" id="reservation">
			                	</div>
				  				
					  		</div>
					  		<div class="form-group margin-top-10 col-md-1">
						  		<button type="submit" class="btn btn-success">Xem</button>
					  		</div>
						</form>
		            </div>
					<div class="row margin-top-10">
						<div class="col-md-10">
			               	<div class="table-responsive" id="divTableData">

			               	</div>
		               	</div>						
					</div>
	            </div>
	            <!-- /.box-body -->         	
            </div>
        </div>
      </div>
      <!-- /.col -->
   </div>
   <!-- /.row -->
</section>
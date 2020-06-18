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
            <div class="box-header">
  	 		   	  <div class="input-group col-md-4 pull-left">
  		            <span class="input-group-addon"><i class="fa fa-search"></i></span>
			            <input data-pageCurrent="<?= $view_data['pageCurrent'] ?>" data-urlSubmit="<?=base_url_admin?>/user/search" type="text" class="form-control" name="keyword" id="keyword" placeholder="Nhập từ khóa và ấn enter để tìm...">
              </div>
              <div class="input-group col-md-1 pull-left">
                  &nbsp;
                  <button data-pageCurrent="<?= $view_data['pageCurrent'] ?>" data-urlSubmit="<?=base_url_admin?>/user/search" type="button" id="btnSearch" class="btn btn-primary">Tìm</button>
              </div>
            </div>
            <div id="box">
        	 	     	
            </div>
        </div>
      </div>
      <!-- /.col -->
   </div>
   <!-- /.row -->
</section>
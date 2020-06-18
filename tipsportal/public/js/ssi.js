var tmr;

function fixSidebar(){
	$(".left_col").css('height',$( document ).height()+"px");
  setTimeout(function(){ $(".left_col").css('height',$( document ).height()+"px"); }, 500);
}

function openMenu(link, el){
	$('#mainContent').load("" + link);
  clearTimeout(tmr);
	//$(".child_menu ul").find ("li").removeClass("current-page");
	//el.className = "current-page";
}

function reloadData(x){
  x.DataTable().ajax.reload()
}

$('.nav_menu').on('click', 'li', function() {
   //console.log($( '.child_menu>li' ));
	$(".child_menu ul").find ("li").removeClass("current-page").removeClass('active');
});


$('.child_menu').on('click', 'li', function() {
    $('.child_menu li').removeClass('current-page').removeClass('active');
    $(this).addClass('current-page');
});


function formatMoneyNoDecimal(x){
  return x.replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

function formatMoney(x){
  var obj = x.split(".");
  obj[0] = obj[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
  return obj.join(".");
}

function justNumber(evt){
  var theEvent = evt || window.event;
  var key = theEvent.keyCode || theEvent.which;
  key = String.fromCharCode( key );
  var regex = /[0-9]|\./;
  if( !regex.test(key) ) {
    theEvent.returnValue = false;
    if(theEvent.preventDefault) theEvent.preventDefault();
  }
}


function justMoney(txt, evt) {

  var charCode = (evt.which) ? evt.which : evt.keyCode;
  if (charCode == 46) {
      //Check if the text already contains the . character
      if (txt.value.indexOf('.') === -1) {
          return true;
      } else {
          return false;
      }
  } else if (charCode == 45) {
      //console.log(txt.value.indexOf('-'));
    if (txt.value.length > 0){
      return false;
    }else{
      if (txt.value.indexOf('-') === -1) {
        return true;
      } else {
        return false;
      }
    }
  } else if (charCode == 43 || charCode == 61){
    return true;
  } else {
      if (charCode > 31
           && (charCode < 48 || charCode > 57))
          return false;
  }
  return true;
}



function chFormat(x){
  var o = x.split("-");
  return o[2]+"-"+o[1]+"-"+o[0];
}

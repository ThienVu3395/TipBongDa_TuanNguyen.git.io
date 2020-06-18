/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	 config.language = 'vi';
	 config.enterMode = CKEDITOR.ENTER_BR;
	// config.uiColor = '#AADC6E';
    config.height = '300px';
	config.toolbar_Full =
	[
	['Source','-','Save','NewPage','Preview','-','Templates'],
	['Cut','Copy','Paste','PasteText','PasteFromWord','-','Print', 'SpellChecker', 'Scayt'],
	['Undo','Redo','-','Find','Replace','-','SelectAll','RemoveFormat'],
	['Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton', 'HiddenField'],
	['Bold','Italic','Underline','Strike','-','Subscript','Superscript'],
	['NumberedList','BulletedList','-','Outdent','Indent','Blockquote','CreateDiv'],
	['JustifyLeft','JustifyCenter','JustifyRight','JustifyBlock'],
	['BidiLtr', 'BidiRtl' ],
	['Link','Unlink','Anchor'],
	['Image','Flash','Table','HorizontalRule','Smiley','SpecialChar','PageBreak','Iframe'],
	['Styles','Format','Font','FontSize'],
	['TextColor','BGColor'],
	['Maximize', 'ShowBlocks','-','About']
	];

	 config.filebrowserBrowseUrl = '/admin/ckfinder/ckfinder.html';
	 config.filebrowserImageBrowseUrl = '/admin/ckfinder/ckfinder.html?type=Images';
	 config.filebrowserFlashBrowseUrl = '/admin/ckfinder/ckfinder.html?type=Flash';
	 config.filebrowserUploadUrl = '/admin/ckfinder/core/connector/php/connector.php?command=QuickUpload&type=Files';
	 config.filebrowserImageUploadUrl = '/admin/ckfinder/core/connector/php/connector.php?command=QuickUpload&type=Images';
	 config.filebrowserFlashUploadUrl = '/admin/ckfinder/core/connector/php/connector.php?command=QuickUpload&type=Flash';

	/*
	config.filebrowserBrowseUrl = '/volampknew/admin/ckfinder/ckfinder.html';
	config.filebrowserImageBrowseUrl = '/volampknew/admin/ckfinder/ckfinder.html?type=Images';
	config.filebrowserFlashBrowseUrl = '/volampknew/admin/ckfinder/ckfinder.html?type=Flash';
	config.filebrowserUploadUrl = '/volampknew/admin/ckfinder/core/connector/php/connector.php?command=QuickUpload&type=Files';
	config.filebrowserImageUploadUrl = '/volampknew/admin/ckfinder/core/connector/php/connector.php?command=QuickUpload&type=Images';
	config.filebrowserFlashUploadUrl = '/volampknew/admin/ckfinder/core/connector/php/connector.php?command=QuickUpload&type=Flash';
	*/
};

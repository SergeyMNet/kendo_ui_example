
//---функция при нажатии на кнопку 'загрузить' - открывается kendo_window и kendo_uloader
var UplFile = function (e) {
        myWindow.data("kendoWindow").close();
        e.preventDefault();
        var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
        var uploadFileName = dataItem.Name;
        
        console.log("uploadFileName = " + uploadFileName);
        
        var text = '<div class="demo-section k-content"><input name="Content" id="kContent" type="file" /></div>'
        $(text).insertAfter("#uploadContent");

        $("#kContent").kendoUpload({
            async: {
                saveUrl: "/Test/UploadFileToDb?docName=" + uploadFileName,
                autoUpload: true
            },
            multiple: false,            
            success: function () {                
                $("#kNewGrid").data("kendoGrid").dataSource.read();
            },
            upload: function(e) {
                var files = e.files;

                $.each(files, function () {
                    if (this.size > 10 * 1024 * 1024) {
                        alert(this.name + " is too big! Max size 10Mb");
                        e.preventDefault(); // This cancels the upload for the file
                    }
                });
            }
        });        
        myWindow.data("kendoWindow").open();
    }
    
//---окно где находится uploader, после закрытия аплодер удаляется
var myWindow = $("#window");

myWindow.kendoWindow({
    width: "600px",
    title: "Upload File",
    visible: false,
    actions: [
        "Close"
    ],
    close: function () {
        $("#window").find(".demo-section.k-content").remove();            
    }
}).data("kendoWindow").center();
    
    

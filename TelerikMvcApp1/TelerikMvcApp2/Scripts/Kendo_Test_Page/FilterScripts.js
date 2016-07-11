$(document).ready(function () {
    var menu = $("#kMenu"),
            original = menu.clone(true);
    var menu2 = $("#kMenu2"),
            original = menu2.clone(true);

    var initMenu = function () {

        menu = $("#kMenu").kendoContextMenu({
            orientation: "vertical",
            target: "#kNewTree",
            filter: ".k-item",
            animation: {
                open: { effects: "fadeIn" },
                duration: 500
            },
            select: function (e) {

                var targItem = e.target.firstChild.innerText;
                console.log("targItem = " + targItem);

                var targAction = e.item.firstChild.innerText;
                console.log("targAction = " + targAction);

                if (targAction === "Edit") {
                    editNameFolder(targItem);
                } else if (targAction === "Add next") {
                    addAfter(targItem);
                } else if (targAction === "Add child") {
                    addChild(targItem);
                } else if (targAction === "Add document") {
                    addDoc(targItem);
                } else if (targAction === "Delete") {
                    remove(targItem);
                }
            }
        });
    };

    var initMenu2 = function () {

        menu2 = $("#kMenu2").kendoContextMenu({
            orientation: "vertical",
            target: "#TreeHead",
            animation: {
                open: { effects: "fadeIn" },
                duration: 500
            },
            select: function (e) {
                var targAction = e.item.firstChild.innerText;
                console.log("targAction = " + targAction);

                if (targAction === "Add new folder") {
                    addAfter("");
                }
            }
        });
    };

    $("#kDropDownTemplate").kendoDropDownList({
        dataTextField: "text",
        dataValueField: "value",
        dataSource: [
            { text: "All", value: 1 },
            { text: "Unapproved", value: 2 },
            { text: "Approved", value: 3 },
            { text: "Archived", value: 4 }
        ]
    });


    //---Status Filter
    $("#kDropDownStatusFilter").kendoDropDownList({
        dataTextField: "text",
        dataValueField: "value",
        dataSource: [
            { text: "All", value: "" },
            { text: "Unapproved", value: "Unapproved" },
            { text: "Approved", value: "Approved" },
            { text: "Archived", value: "Archived" }
        ],
        change: function () {
            //--todo action
            console.log("ActionStatus result = " + this.value());
            targetFilterStatus = this.value();
            $("#kNewGrid").data("kendoGrid").dataSource.read();
        }
    });

    //---DateTime Filter
    $("#kDateFilter").kendoDatePicker({
        value: "1.1.2016",
        format: "dd.MM.yyyy",
        change: function () {
            //--todo action
            console.log("ActionDate result = " + this.value());
            targetFilterDate = this.value();
            $("#kNewGrid").data("kendoGrid").dataSource.read();
        }
    });

    //---Name Filter
    $("#kStringNameFilter").kendoAutoComplete({
        placeholder: "serch document...",
        filter: "startswith",
        dataSource: dataAuto,
        change: function () {
            //--todo action
            console.log("ActionNameFilter result = " + this.value());
            targetFilterName = this.value();
            $("#kNewGrid").data("kendoGrid").dataSource.read();
        }
    });

    initMenu();
    initMenu2();


    var dataAuto = [];

});
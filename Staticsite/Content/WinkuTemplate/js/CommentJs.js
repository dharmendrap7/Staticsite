//<script src="http://code.jquery.com/jquery-1.11.3.min.js"></script>

// comment script-------------
$(document).ready(function () {
    debugger;
    $(".comment").on("click", function (ref) {
        debugger;

        $('#PostId').val('');

        $('#IsReplay').val('')
        $('#IsReplay').val(0)
        var id1 = $(this).attr("id");
        $('#pcid').val('');
        $('#pcid').val(id1);
        $('#PostId').val(id1);
        alert($('#pcid').val() + ' postid:' + $('#PostId').val());
        $.ajax({
            type: "GET",
            //url: '@Url.Action("GetComments", "BasicFunctionality")',
            url: '/BasicFunctionality/GetComments',
            data: { postId: id1 },
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (response) {
                debugger;
                $('.del').remove();
                var divid = 'postid_' + id1;
                $('#' + 'postid_' + id1).after(response);
                scroll();


            },
            failure: function (response) {
                debugger;
                alert(response.responseText);
            },
            error: function (response) {
                debugger;
                alert(response.responseText);
            }
        });

    });
});


function scroll() {
    debugger;
    alert("OutOFScroll")
    $('.cmt, .followers').perfectScrollbar();
    if ($.isFunction($.fn.perfectScrollbar)) {

        alert("Scrpller123")
        $('.cmt, .followers').perfectScrollbar();
    }
}

var name = "";
var apendParent = ""
function ReplyTo(el) {
    debugger;
    alert($('#IsReplay').val())
    $('#IsReplay').val('')
    $('#IsReplay').val(1)
    var cmtId = $(el).attr("id");
    name = ($(el).parent()).children().children().text();
    $('#pcid').val('');
    $('#pcid').val(cmtId);
    $(".replyto").html('');
    $(".replyto").text('Reply to: ' + name);
    alert($('#pcid').val())
    apendParent = ($(el).parent()).parent();

}

function ShowReply(el) {
    debugger;
    var cmtId = $(el).attr("id");
    name = "";
    name = ($(el).parent()).children().children().text();
    // $('#pcid').val('');
    // $('#pcid').val(cmtId);
    alert($('#pcid').val())
    $.ajax({
        type: "GET",
        //url: '@Url.Action("GetSubComments", "BasicFunctionality")',
        url: '/BasicFunctionality/GetSubComments',
        data: { ComID: cmtId },
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            debugger;
            // $('.del').remove();
            // $('div [class=del]').remove();
            $(".replyto").html();
            // $(".replyto").text('Reply to:');
            ($(el).parent()).parent().nextAll('.subul').remove();
            ($(el).parent()).parent().after(response);

            // jQuery("time.timeago").timeago();
            // $('#dialog').dialog('open');
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

/** Post a Comment **/
$(document).ready(function () {
    jQuery(".post-comt-box textarea").on("keydown", function (event) {

        if (event.keyCode == 13) {
            debugger;
            alert("posting comment..")
            var comment = jQuery(this).val();
            var pcid = $('#pcid').val();
            var IsReplay = $('#IsReplay').val();
            var URL = "";
            if (IsReplay == 1) {
                // URL = '@Url.Action("AddSubComment", "BasicFunctionality")'
                URL = '/BasicFunctionality/AddSubComment'
            }
            else {
                // URL = '@Url.Action("AddComment", "BasicFunctionality")'
                URL = '/BasicFunctionality/AddComment'
            }
            alert($('#pcid').val());
            alert(comment);
            alert("Destination ID"+$('#detinationId').val()); 
            $.ajax({
                type: "POST",
                url: URL,
                data: { commentMsg: comment, postorcmtId: $('#pcid').val(),destinationId:$('#detinationId').val() },
                // contentType: "application/json; charset=utf-8",
                //  dataType: "json",
                success: function (response) {
                    debugger;
                    jQuery(this).val('');
                    if (response._result == true && IsReplay != 1) {
                        debugger;

                        var parent = jQuery(".showmore").parent("li");
                        alert(JSON.stringify(parent));
                        //  $(response).insertBefore(parent);
                        var comment_HTML = '	<li><div class="comet-avatar"><img src="images/resources/comet-1.jpg" alt=""></div>'
                            + '<div class="we-comment" > <div class="coment-head"><h5><a href="time-line.html" title="">' + response.Name + '</a></h5><span>1 year ago</span>'
                            + '<a class="we-reply" href = "#" title = "Reply" > <i class="fa fa-reply"></i></a ></div > <p>' + comment + '</p></div ></li > ';

                        var cmd_html = '<li><div class="comet-avatar"><img src="~/Content/assets/images/resources/comet-2.jpg" alt=""> </div>'
                            + '<div class="we-comment"><div class="coment-head">'
                            + '<h5><a href="time-line.html" title="">'
                            + response.Name + '</a></h5>'
                            + '<span>1 month ago' + response.CDate + '</span>'
                            + '<a class="we-reply" id="' + response.CId + '" onclick="ReplyTo(this);" href="javascript:void(0);" title="Reply">'
                            + '<i class="fa fa-reply"></i></a>'
                            + '<a class="we-reply" title="show Reply" id="' + response.CId + '" onclick="ShowReply(this);" href="javascript:void(0);">'
                            + '<i class="fa fa-arrow-circle-down"></i></a></div><p>'
                            + comment + '</p></div> </li>'
                        $(cmd_html).insertBefore(parent);
                        jQuery(this).val('');
                        var postid = $('#PostId').val();
                        alert(postid);
                        $('#pcid').val(postid);
                        $('.post-comt-box textarea').val('');
                    }
                    else if (response._result == true && IsReplay == 1) {
                        debugger;
                        var cmd_html = '';
                        var comment_HTML = '	<li><div class="comet-avatar"><img src="images/resources/comet-1.jpg" alt=""></div>'
                            + '<div class="we-comment" > <div class="coment-head"><h5><a href="time-line.html" title="">' + name + '</a></h5><span>1 year ago</span>'
                            + '<a class="we-reply" href = "#" title = "Reply" > <i class="fa fa-reply"></i></a ></div > <p>' + comment + '</p></div ></li > ';
                        if (apendParent.siblings('.subul').length > 0) {
                            alert("ul present..")
                            cmd_html = '<li><div class="comet-avatar"><img src="~/Content/assets/images/resources/comet-2.jpg" alt=""> </div>'
                                + '<div class="we-comment"><div class="coment-head">'
                                + '<h5><a href="time-line.html" title="">'
                                + response.Name + '</a></h5>'
                                + '<span>1 month ago' + response.CDate + '</span>'
                                + '<a class="we-reply" id="' + response.CId + '" onclick="ReplyTo(this);" href="javascript:void(0);" title="Reply">'
                                + '<i class="fa fa-reply"></i></a>'
                                + '<a class="we-reply" title="show Reply" id="' + response.CId + '" onclick="ShowReply(this);" href="javascript:void(0);">'
                                + '<i class="fa fa-arrow-circle-down"></i></a></div><p>'
                                + comment + '</p></div> </li>'
                            apendParent.siblings('.subul').append(cmd_html);
                        }
                        else {
                            alert("no ul...")
                            cmd_html = '<ul class="subul"><li><div class="comet-avatar"><img src="~/Content/assets/images/resources/comet-2.jpg" alt=""> </div>'
                                + '<div class="we-comment"><div class="coment-head">'
                                + '<h5><a href="time-line.html" title="">'
                                + response.Name + '</a></h5>'
                                + '<span>1 month ago' + response.CDate + '</span>'
                                + '<a class="we-reply" id="' + response.CId + '" onclick="ReplyTo(this);" href="javascript:void(0);" title="Reply">'
                                + '<i class="fa fa-reply"></i></a>'
                                + '<a class="we-reply" title="show Reply" id="' + response.CId + '" onclick="ShowReply(this);" href="javascript:void(0);">'
                                + '<i class="fa fa-arrow-circle-down"></i></a></div><p>'
                                + comment + '</p></div> </li></ul>'
                            $(cmd_html).insertAfter(apendParent);
                        }

                        var postid = $('#PostId').val();
                        alert(postid);
                        $('#pcid').val(postid);
                        $('.post-comt-box textarea').val('');
                        $('#IsReplay').val('')
                        $('#IsReplay').val(0);
                        $(".replyto").html('');
                    }
                    else if (response._result == false) {
                        alert("Error");
                    }

                },
                failure: function (response) {
                    alert(response.responseText);
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });

        }
    });


});

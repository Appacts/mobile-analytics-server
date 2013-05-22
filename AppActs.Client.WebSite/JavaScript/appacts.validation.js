function validateField(sender, args) {
    if (!args.Value || args.Value.length == 0) {
        $("#".concat(sender.controltovalidate)).parent().find('.Title').addClass('TitleError');
        args.IsValid = false;
    }
    else {
        $("#".concat(sender.controltovalidate)).parent().find('.TitleError').removeClass('TitleError');
    }
};
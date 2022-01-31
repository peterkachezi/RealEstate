$(document).ready(function () {

    $('#datepicker-inline').datepicker({
        todayHighlight: true,
        dateFormat: 'yy-mm-dd',
        //minDate: dateToday,
        minDate: new Date("02/03/2021"),
        onSelect: function (date) {

            //console.log("Selected date: " + date + "; input's current value: " + this.value);

            var calendarContainer = $("#datepicker-inline");

            var card = calendarContainer.closest(".card");

            var selectedDate = card.find(".selectedDate");

            var today = new Date();

            selectedDate.html(moment(date).format('dddd MMMM D Y') + " Slots");
            //if (selectedDate);

            $("#AppointmentClinic").html($("select[name='Patients[0]\\.ClinicId'] :selected").text());

            $("#AppointmentDate").html(moment(date).format('dddd MMMM D Y'));

            $("#AppointmentTime").html($("input[name='Patients[0]\\.AppointmentTime']").val());

            var d = new Date(date);

            d.setHours(0, 0, 0, 0);

            if (d.getTime() == today.getTime()) {

                slotsforToday(card);

            }

            else {

                slotsfornextdays(d, card);

            }
        }
    });

    //calendar 0
    var defaultConfig0 = {

        date: new Date(),
        weekDayLength: 3,
        disable: function (date) {
            return date < new Date(new Date().getTime() - (1 * 24 * 60 * 60 * 1000));
        },
        onClickDate: function (date) {

            var calendarContainer = $("#calendar-container0");

            var card = calendarContainer.closest(".card");

            var selectedDate = card.find(".selectedDate");

            var today = new Date();

            today.setHours(0, 0, 0, 0);

            calendarContainer.updateCalendarOptions({
                date: date
            });

            selectedDate.html(moment(date).format('dddd MMMM D Y') + " Slots");

            $("#AppointmentClinic").html($("select[name='Patients[0]\\.ClinicId'] :selected").text());

            $("#AppointmentDate").html(moment(date).format('dddd MMMM D Y'));

            $("#AppointmentTime").html($("input[name='Patients[0]\\.AppointmentTime']").val());

            var d = new Date(date);

            d.setHours(0, 0, 0, 0);

            if (d.getTime() == today.getTime()) {

                slotsforToday(card);

            } else {

                slotsfornextdays(d, card);

            }
        },
        showYearDropdown: false,
    };

    $('#calendar-container0').calendar(defaultConfig0);


    var current_fs, next_fs, previous_fs; //fieldsets
    var opacity;

    function AddParentPatient(e) {

        var $this = e;

        $("#loadMe").modal({
            show: true
        });

        var form = $("#msform");

        var formData = new FormData(form[0]);

        return $.ajax({
            type: "POST",
            enctype: 'multipart/form-data',
            url: "/home/AddAppointment",
            data: formData,
            processData: false,
            contentType: false,
            //cache: false,
            timeout: 800000,
            success: function (data) {

                setTimeout(function () {

                    $("#loadMe").modal("hide");

                }, 3000);

                if (data.Item1 != true) {

                    $("#step-1-alert").show();

                } else {

                    $("#step-1-alert").show();

                    $("input[name='Patients[0]\\.Id']").val(data.Item2.Patients[0].Id);

                    var patientDTO = data.Item2.Patients[0];

                    $("#SummaryFullName").html(patientDTO.FirstName + " " + (patientDTO.MiddleName == null ? "" : patientDTO.MiddleName) + " " + patientDTO.LastName);

                    $("#SummaryIdNumber").html(patientDTO.IdNumber);

                    $("#SummaryAppointmentNumber").html(patientDTO.IdNumber);

                    $(".MpesaAccountNumber").html("Enter your account number <strong>" + patientDTO.IdNumber + "</strong>");

                    $("#SummaryPhoneNumber").html(patientDTO.PhoneNumber);

                    $("#SummaryDateOfBirth").html(patientDTO.DateOfBirthStr);

                    $("#SummaryGender").html(patientDTO.GenderDescription);

                    $("#SummaryEmailAddress").html(patientDTO.EmailAddress);

                    $("#SummaryNationality").html(patientDTO.Nationality);

                    var contraIndications = patientDTO.PatientContraIndications;

                    for (var r = 0; r < contraIndications.length; r++) {

                        var row = $("<div class=\"row\"><div class=\"col-md-8\" ><p>" + contraIndications[r].Description + "</p></div> <div class=\"col-md-4\"><div class=\"form-check\"><div class=\"form-check form-check-inline custom-position\"><label class=\"radio radio-before\"><span class=\"radio__label\">" + contraIndications[r].AnswerDescription + "</span></label></div></div></div></div >");

                        $("#SummaryContraIndications").append(row);

                    }

                    var numberOfOtherPersons = Number($("input[name='OtherPersons']").val());

                    if (numberOfOtherPersons > 1) {

                        nextStep($this);

                        $("#medicalhistory").css("display", "block");

                    } else {

                        nextStep($this);

                        nextStep($("input[step='2']"));

                        MpesaExpressRequest($("button[name='submit'][step='4']"));
                    }
                }
            }
        })
    }

    function AddMedicalHistories(e) {

        var $this = e;

        $("#loadMe").modal({
            show: true
        });

        var form = $("#msform");

        var formData = new FormData(form[0]);

        return $.ajax({
            type: "POST",
            enctype: 'multipart/form-data',
            url: "/home/AddMedicalHistories",
            data: formData,
            processData: false,
            contentType: false,
            //cache: false,
            timeout: 800000,
            success: function (data) {

                setTimeout(function () {

                    $("#loadMe").modal("hide");

                }, 4000);

                if (data.Item1 != true) {

                    $("#step-2-alert").show();

                } else {

                    $("#step-2-alert").show();

                    for (var r = 0; r < data.Item2.Patients.length; r++) {

                        $("input[name='Patients[" + r + "]\\.Id']").val(data.Item2.Patients[r].Id);

                    }

                    nextStep($this);

                    MpesaExpressRequest($("button[name='submit'][step='4']"));

                    var patientDTO = data.Item2.Patients[0];

                    var medicalHistories = patientDTO.PatientMedicalHistories;

                    if (medicalHistories != null) {

                        for (var r = 0; r < medicalHistories.length; r++) {

                            var row = $("<div class=\"row\"><div class=\"col-md-8\" ><p>" + medicalHistories[r].MedicalHistoryDescription + "</p></div> <div class=\"col-md-4\"><div class=\"form-check\"><div class=\"form-check form-check-inline custom-position\"><label class=\"radio radio-before\"><span class=\"radio__label\">" + medicalHistories[r].AnswerDescription + "</span></label></div></div></div></div >");

                            $("#SummaryMedicalHistories").append(row);

                        }
                    }

                    var patients = data.Item2.Patients;

                    if (patients != null) {

                        for (var r = 0; r < patients.length; r++) {

                            if (r > 0) {

                                $("input[name='Patients[" + r + "]\\.Id']").val(data.Item2.Patients[r].Id);

                                var row = "<div class=\"form-row\"> <div class=\"form-group col-md-3\"> <label for=\"fullname\">Full Name : </label> <label id=\"\">" + patients[r].Name + "</label> </div> <div class=\"form-group col-md-3\"> <label for=\"dob\">Date Of Birth : </label> <label id=\"\">" + patients[r].DateOfBirthStr + "</label> </div> </div><div class=\"form-row\"> <div class=\"form-group col-md-3\"> <label for=\"fullname\">Gender : </label> <label id=\"\">" + patients[r].GenderDescription + "</label> </div> <div class=\"form-group col-md-3\"> <label for=\"fullname\">Id Number : </label> <label id=\"\">" + patients[r].IdNumber + "</label> </div> </div>";

                                $("#OtherPersonsDetails").append(row);

                            }

                        }

                    }

                }
            }
        })
    }

    function AddAppointmentSlots(e) {

        var $this = e;

        $("#loadMe").modal({
            show: true
        });

        var form = $("#msform");

        var formData = new FormData(form[0]);

        return $.ajax({
            type: "POST",
            enctype: 'multipart/form-data',
            url: "/home/AddAppointmentSlots",
            data: formData,
            processData: false,
            contentType: false,
            //cache: false,
            timeout: 800000,
            success: function (data) {

                $("#loadMe").modal("hide");

                if (data.Item1 != true) {

                    $("#step-3-alert").show();

                } else {

                    $("input[name='Patients[0]\\.AppointmentId']").val(data.Item2.Patients[0].AppointmentId);

                    $("#step-3-alert").show();

                    $("#SummaryAppointmentDate").html(data.Item2.Patients[0].AppointmentDateStr);
                    $("#SummaryAppointmentClinic").html(data.Item2.Patients[0].ClinicName);
                    $("#SummaryAppointmentTime").html(data.Item2.Patients[0].AppointmentStartTimeStr + " - " + data.Item2.Patients[0].AppointmentEndTimeStr);

                    nextStep($this);
                }
            }
        })
    }

    function AddFirstAndSecondAppointmentSlots(e) {

        var $this = e;

        $("#loadMe").modal({
            show: true
        });

        var form = $("#msform");

        var formData = new FormData(form[0]);

        return $.ajax({
            type: "POST",
            enctype: 'multipart/form-data',
            url: "/home/AddFirstAndSecondAppointmentSlots",
            data: formData,
            processData: false,
            contentType: false,
            //cache: false,
            timeout: 800000,
            success: function (data) {

                $("#loadMe").modal("hide");

                if (data.Item1 != true) {

                    $("#step-3-alert").show();

                } else {


                    $("input[name='Patients[0]\\.AppointmentId']").val(data.Item2.Patients[0].AppointmentId);
                    $("input[name='Patients[0]\\.NextAppointmentId']").val(data.Item2.Patients[0].NextAppointmentId);

                    $("#step-3-alert").show();

                    $("#SummaryAppointmentDate").html(data.Item2.Patients[0].AppointmentDateStr);
                    $("#SummaryAppointmentClinic").html(data.Item2.Patients[0].ClinicName);
                    $("#SummaryAppointmentTime").html(data.Item2.Patients[0].AppointmentStartTimeStr + " - " + data.Item2.Patients[0].AppointmentEndTimeStr);

                    nextStep($this);
                }
            }
        })
    }

    function nextStep(e) {

        $this = e;

        current_fs = $this.closest("fieldset");

        next_fs = $this.closest("fieldset").next();

        //Add Class Active
        $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");
        $("#seconddoseprogressbar li").eq($("fieldset").index(next_fs)).addClass("active");

        //show the next fieldset
        next_fs.show();
        //hide the current fieldset with style
        current_fs.animate({ opacity: 0 }, {
            step: function (now) {
                // for making fielset appear animation
                opacity = 1 - now;

                current_fs.css({
                    'display': 'none',
                    'position': 'relative'
                });
                next_fs.css({ 'opacity': opacity });
            },
            duration: 600
        });

    }

    $(".next").click(function () {

        //nextStep($(this));

        var fieldset = $(this).closest("fieldset");

        var valid = true;

        fieldset.find('input:not(:button,:disabled,:checkbox,.multiselect-search), select:not(.multi-select,.multiselect-search,:disabled)').each(function (index, value) {

            if ($(this).parsley().validate() != true) {

                valid = false;
            }

        });

        if (!valid) {

            swal("Sorry", "You have to provide details marked in red", "error");

            $([document.documentElement, document.body]).animate({
                scrollTop: $(".img").offset().top
            }, 2000);

            return;
        }

        var step = $(this).attr("step");

        if (step == "1") {

            var contraIndications = 0;

            var checkedContraIndications = 0;

            $(".ContraIndicationAnswer").each(function () {

                var tobechecked = $(this).attr("tobechecked");

                if (tobechecked == "1") {

                    contraIndications++;

                    if ($(this).is(":checked")) {

                        checkedContraIndications++;

                    }

                }

            });

            var RTPCR = 0;

            $(".RTPCR").each(function () {

                if ($(this).is(":checked")) {
                    RTPCR++;
                }

            });

            if (RTPCR != 1) {

                swal("Sorry", "You have to answer RT-PCR test quetion before yu proceed!", "error");

                $([document.documentElement, document.body]).animate({
                    scrollTop: $(".img").offset().top
                }, 2000);

                return;
            }


            if ((contraIndications / 2) != checkedContraIndications) {

                swal("Sorry", "You have to answer all contra indications", "error");

                $([document.documentElement, document.body]).animate({
                    scrollTop: $(".img").offset().top
                }, 2000);

                return;
            }

            AddParentPatient($(this));

        }

        if (step == "2") {

            var medicalHistories = 0;

            var checkedMedicalHistories = 0;

            $(".MedicalHistoryAnswer").each(function () {

                var tobechecked = $(this).attr("tobechecked");

                if (tobechecked == "1") {

                    medicalHistories++;

                    if ($(this).is(":checked")) {

                        checkedMedicalHistories++;

                    }
                }

            });

            if ((medicalHistories / 2) != checkedMedicalHistories) {

                swal("Sorry", "You have to answer all medical history questions", "error");

                return;
            }

            AddMedicalHistories($(this));
        }

        if (step == "3") {

            var appointmentTime = $("input[name='Patients[0]\\.AppointmentTime']").val();

            var appointmentDate = $("input[name='Patients[0]\\.AppointmentDate']").val();

            if (appointmentTime == null || appointmentTime == "" || appointmentDate == null || appointmentDate == "") {

                swal("Sorry!", "You must select a time slot before you proceed", "error")

            } else {

                var dose = $("input[name='DoseBooking']").val();

                if (dose == "1") {
                    AddAppointmentSlots($(this));
                }

                if (dose == "3") {
                    AddFirstAndSecondAppointmentSlots($(this));
                }

                setTimeout(function () {

                    $("#loadMe").modal("hide");

                }, 3000);

            }
        }

        if (step == "4") {

            nextStep($(this));

        }

    });

    $(".previous").click(function () {

        current_fs = $(this).parent();
        previous_fs = $(this).parent().prev();

        //Remove class active
        $("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");
        $("#seconddoseprogressbar li").eq($("fieldset").index(current_fs)).removeClass("active");

        //show the previous fieldset
        previous_fs.show();

        //hide the current fieldset with style
        current_fs.animate({ opacity: 0 }, {
            step: function (now) {
                // for making fielset appear animation
                opacity = 1 - now;

                current_fs.css({
                    'display': 'none',
                    'position': 'relative'
                });
                previous_fs.css({ 'opacity': opacity });
            },
            duration: 600
        });
    });

    $('.radio-group .radio').click(function () {
        $(this).parent().find('.radio').removeClass('selected');
        $(this).addClass('selected');
    });

    $(".submit").click(function () {
        return false;
    })
    // Safari 3.0+ "[object HTMLElementConstructor]" 
    var isSafari = /constructor/i.test(window.HTMLElement) || (function (p) { return p.toString() === "[object SafariRemoteNotification]"; })(!window['safari'] || (typeof safari !== 'undefined' && safari.pushNotification));
    console.log(isSafari)
    if (isSafari) {
        $("#calendar-container0").datepicker();
        $("#calendar-container0").datepicker("setDate", new Date());
        $("##calendar-container0").datepicker('show');
    }
});

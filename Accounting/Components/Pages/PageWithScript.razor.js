
export function onLoad() {
    alert("Hello Load");
  console.log('Loaded');
/* Select your element */
    var mainInput = document.getElementById("nepaliDate");

/* Initialize Datepicker with options */
    mainInput.nepaliDatePicker();
    console.log(mainInput)

}

export function onUpdate() {
    alert("Hello Update");
    console.log('Updated');
    var mainInput = document.getElementById("nepaliDate");

    /* Initialize Datepicker with options */
    mainInput.nepaliDatePicker();
    console.log(mainInput)

}

export function onDispose() {
    alert("Hello Dispose");
    console.log('Disposed');
    var mainInput = document.getElementById("nepaliDate");

    /* Initialize Datepicker with options */
    mainInput.nepaliDatePicker();
    console.log(mainInput)
}
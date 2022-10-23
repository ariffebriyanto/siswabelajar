/* Autonumeric */
try {
    new AutoNumeric('.jsAutonumeric', {
        minimumValue: "0",
        maximumValue: "100000000",
        decimalPlaces: '0',
        decimalCharacter: ',',
        digitGroupSeparator: '.',
        selectOnFocus: false,
        modifyValueOnWheel: false,
        onInvalidPaste: "ignore",
    });
} catch {

}
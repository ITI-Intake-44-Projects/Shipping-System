import { AbstractControl, ValidatorFn, ValidationErrors } from '@angular/forms';

// Custom validator for email format
export function emailValidator(control: AbstractControl): ValidationErrors | null {
  // Regex pattern for email validation
  const pattern: RegExp = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
  if (!pattern.test(control.value)) {
    return { invalidEmail: true };
  }
  return null;
}

// Custom validator for password format (minimum 8 characters, at least one uppercase letter, one lowercase letter, one number, and one special character)
export function passwordValidator(control: AbstractControl): ValidationErrors | null {
  const pattern: RegExp = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
  if (!pattern.test(control.value)) {
    return { invalidPassword: true };
  }
  return null;
}

// Custom validator for username (no spaces)
export function noSpacesValidator(control: AbstractControl): ValidationErrors | null {
  if (control.value && control.value.indexOf(' ') >= 0) {
    return { noSpaces: true };
  }
  return null;
}

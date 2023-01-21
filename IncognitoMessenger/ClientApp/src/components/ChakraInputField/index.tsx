import { FormControl, FormLabel, Input, FormErrorMessage, StyleProps } from "@chakra-ui/react";
import { Field, FieldProps } from "formik";
import { HTMLInputTypeAttribute } from "react";

type Props = StyleProps & {
  name: string,
  label?: string,
  placeholder?: string
  isValidate?: boolean,
  type?: HTMLInputTypeAttribute 
};

const ChakraInputField = ({name, label, placeholder, isValidate = false, type = "text", ...props}: Props) => {
  return (
    <Field name={name}>
      {({ field, meta }: FieldProps) => {
        const validateProp = {isInvalid: !!meta.error && !!meta.touched};

        return (
          <FormControl 
            {...props} 
            {...(isValidate ? validateProp : null)}
          >
            {label && <FormLabel>{label}</FormLabel>}
            <Input {...field} placeholder={placeholder} type={type}/>
            {isValidate && <FormErrorMessage>{meta.error}</FormErrorMessage>}
          </FormControl>
        );
      }}
    </Field>
  );
};

export default ChakraInputField;
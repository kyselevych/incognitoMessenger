import { FormControl, FormLabel, Input, FormErrorMessage, StyleProps } from "@chakra-ui/react";
import { Field, type FieldProps } from "formik";

type Props = StyleProps & {
  name: string,
  label?: string,
  placeholder?: string
  isValidate?: boolean
};

const ChakraInputField = ({name, label, placeholder, isValidate = false, ...props}: Props) => {
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
            <Input {...field} placeholder={placeholder} />
            {isValidate && <FormErrorMessage>{meta.error}</FormErrorMessage>}
          </FormControl>
        );
      }}
    </Field>
  );
};

export default ChakraInputField;
import {
    Dialog,
    DialogTitle,
    DialogContent,
    DialogActions,
    Button,
    TextField,
    Stack,
    MenuItem,
} from "@mui/material";
import { useCreateCategoria } from "../hooks/useCreateCategoria";
import { useToast } from "../../../app/feedbackProvider";
import { extractApiError } from "../../../api/interceptors";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm, Controller } from "react-hook-form";
import { createCategoriaSchema, type CreateCategoriaFormData, type CreateCategoriaFormInput } from "../schemas/createCategoriaSchema";
import { finalidadeOptions } from "../../../domain/constants/finalidadeOptions";

interface Props {
    open: boolean;
    onClose: () => void;
}
export function CreateCategoriaDialog({ open, onClose }: Props) {
    const { mutateAsync, isPending } = useCreateCategoria();
    const { showToast } = useToast();

    const {
        register,
        handleSubmit,
        control,
        reset,
        formState: { errors, isSubmitting },
    } = useForm<CreateCategoriaFormInput>({
        resolver: zodResolver(createCategoriaSchema),
        defaultValues: {
            descricao: "",
            finalidade: undefined
        }
    });

    async function onSubmit(data: CreateCategoriaFormInput) {
        try {
            await mutateAsync(data as CreateCategoriaFormData);
            showToast("Categoria criada com sucesso", "success");
            reset({
                descricao: "",
                finalidade: undefined
            });
            onClose();
        } catch (err: any) {
            showToast(extractApiError(err), "error");
        }
    }

    function handleClose() {
        reset({
            descricao: "",
            finalidade: undefined
        });
        onClose();
    }

    return (
        <Dialog
            open={open}
            onClose={handleClose}
            fullWidth
            maxWidth="sm"
            keepMounted={false}
        >
            <DialogTitle>Nova Categoria</DialogTitle>
            <form onSubmit={handleSubmit(onSubmit)}>
                <DialogContent>
                    <Stack spacing={2} mt={1}>
                        <TextField
                            label="Descrição"
                            {...register("descricao")}
                            error={!!errors.descricao}
                            helperText={errors.descricao?.message}
                        />
                        <Controller
                            name="finalidade"
                            control={control}
                            render={({ field, fieldState }) => (
                                <TextField
                                    select
                                    label="Finalidade"
                                    value={field.value ?? ""}
                                    onChange={(e) => field.onChange(Number(e.target.value))}
                                    error={!!fieldState.error}
                                    helperText={fieldState.error?.message}
                                >
                                    {finalidadeOptions.map((opt) => (
                                        <MenuItem key={opt.value} value={opt.value}>
                                            {opt.label}
                                        </MenuItem>
                                    ))}
                                </TextField>
                            )}
                        />
                    </Stack>
                </DialogContent>
                <DialogActions>
                    <Button
                        onClick={handleClose}
                        color="inherit"
                        variant="outlined"
                    >
                        Cancelar
                    </Button>

                    <Button
                        type="submit"
                        disabled={isSubmitting || isPending}
                    >
                        {isSubmitting || isPending ? "Salvando..." : "Salvar"}
                    </Button>
                </DialogActions>
            </form>
        </Dialog >
    );
}
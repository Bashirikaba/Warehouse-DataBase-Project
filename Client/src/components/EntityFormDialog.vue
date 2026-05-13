<script setup lang="ts">
import Api from '@/apiProvider/api'
import { RouteTypeEnum } from '@/types/enums'
import type { IEntity, ITableConfigItem } from '@/types/interfaces'
import type { Service } from '@/types/types'
import { useToast, type ToastServiceMethods } from 'primevue'
import { type Reactive, reactive, type Ref, ref, watch } from 'vue'

interface IEntityFormDialogProps {
  editData?: IEntity
  config: ITableConfigItem[]
  apiService: Service
}

const emit = defineEmits<{
  (e: 'update'): void
}>()

const visible = defineModel<boolean>({ required: true })
const props = defineProps<IEntityFormDialogProps>()
const label: Ref<string> = ref('')
const entity: Ref<IEntity> = ref({} as IEntity)
const nestedEntities: Ref<Map<Service, IEntity[]>> = ref(new Map<Service, IEntity[]>())
const toast: ToastServiceMethods = useToast()
const errors: Reactive<Record<string, boolean>> = reactive<Record<string, boolean>>({})

Api.setService(props.apiService)

watch(
  () => [visible.value, props.editData],
  () => {
    if (visible.value) {
      label.value = (props.editData ? 'Редактирование' : 'Создание') + ' записи'
      setNestedEntities()
      if (props.editData) {
        entity.value = { ...props.editData }
      } else {
        entity.value = {} as IEntity
      }
    }
  },
  { immediate: true, deep: true },
)

// eslint-disable-next-line @typescript-eslint/no-explicit-any
function getNestedValue(obj: any, path: string) {
  if (!obj || !path) return false
  return path.split('.').reduce((current, key) => {
    return current?.[key] ?? ''
  }, obj)
}

// eslint-disable-next-line @typescript-eslint/no-explicit-any
function getNestedEntity(obj: any, path: string) {
  if (!obj || !path) return ''
  return obj[path.split('.')[0]!]
}

function getNestedEntityLabel(path: string) {
  return path.split('.')[1]
}

function getNestedEntityOptions(nestedEntity: Service) {
  return nestedEntities.value.get(nestedEntity)
}

async function setNestedEntities() {
  const tasks: { key: Service; promise: Promise<IEntity[]> }[] = []

  props.config.forEach((item) => {
    if (item.NestedEntity) {
      nestedEntities.value.set(item.NestedEntity, [])
      Api.setService(item.NestedEntity)
      const promise: Promise<IEntity[]> = Api.getEntity<IEntity>()
      tasks.push({ key: item.NestedEntity, promise })
    }
  })
  Api.setService(props.apiService)

  const results = await Promise.all(tasks.map((task) => task.promise))

  results.forEach((data, index) => {
    const key = tasks[index]!.key
    nestedEntities.value.set(key, data)
  })
}

// eslint-disable-next-line @typescript-eslint/no-explicit-any
function setNestedValue(obj: any, path: string, value: any) {
  const parts = path.split('.')

  for (let i = 0; i < parts.length - 1; i++) {
    if (!obj[parts[i]!]) {
      obj[parts[i]!] = {}
    }
    obj = obj[parts[i]!]
  }

  obj[parts[parts.length - 1]!] = value
}

// eslint-disable-next-line @typescript-eslint/no-explicit-any
function setNestedEntity(obj: any, path: string, value: IEntity) {
  obj[path.split('.')[0]!] = value
}

const validateForm = (): boolean => {
  const newErrors: Record<string, boolean> = {}
  let isValid = true

  props.config
    .filter((item) => item.Field !== 'Id')
    .forEach((item) => {
      const value = getNestedValue(entity.value, item.Field)

      if (!value || (typeof value === 'string' && !value.trim())) {
        newErrors[item.Field] = true
        isValid = false
      } else {
        newErrors[item.Field] = false
      }
    })

  Object.assign(errors, newErrors)

  if (!isValid) {
    toast.add({
      severity: 'error',
      summary: 'Ошибка валидации',
      detail: 'Заполните все поля',
      life: 3000,
    })
  }

  return isValid
}

async function save() {
  if (!validateForm()) return

  if (props.editData) {
    await Api.updateEntity(entity.value)
  } else {
    await Api.createEntity(entity.value)
  }

  visible.value = false
  emit('update')
}

function cancel() {
  visible.value = false
}
</script>
<template>
  <div>
    <Toast position="bottom-left" />
    <Dialog
      :visible="visible"
      close-on-escape
      :closable="false"
      dismissable-mask
      :style="{ width: '450px' }"
      :header="label"
      modal
    >
      <Divider></Divider>
      <div class="fields">
        <div v-for="row in config.filter((r) => r.Field != 'Id')" :key="row.Field">
          <FloatLabel variant="on">
            <InputText
              required
              v-if="row.Type == 'string' && !row.NestedEntity"
              :id="row.Field"
              fluid
              :modelValue="getNestedValue(entity, row.Field)"
              @update:model-value="(val) => setNestedValue(entity, row.Field, val)"
            />
            <Select
              required
              v-if="row.Type == 'string' && row.NestedEntity"
              :id="row.Field"
              fluid
              :modelValue="getNestedEntity(entity, row.Field)"
              :options="getNestedEntityOptions(row.NestedEntity)"
              :option-label="getNestedEntityLabel(row.Field)"
              @update:model-value="(val) => setNestedEntity(entity, row.Field, val)"
            />
            <InputNumber
              required
              v-if="row.Type == 'number' && !row.Enum"
              :id="row.Field"
              fluid
              :max-fraction-digits="row.Numeric ? 2 : 0"
              :modelValue="getNestedValue(entity, row.Field)"
              @update:model-value="(val) => setNestedValue(entity, row.Field, val)"
            />
            <Select
              required
              v-if="row.Type == 'number' && row.Enum"
              :id="row.Field"
              style="width: 100%"
              :modelValue="getNestedValue(entity, row.Field)"
              :options="Object.values(RouteTypeEnum).filter((v) => typeof v == 'number')"
              @update:model-value="(val) => setNestedValue(entity, row.Field, val)"
            />
            <DatePicker
              v-if="row.Type == 'date'"
              :id="row.Field"
              fluid
              show-icon
              :max-date="new Date()"
              :modelValue="getNestedValue(entity, row.Field)"
              @update:model-value="(val) => setNestedValue(entity, row.Field, val)"
            />
            <label :for="row.Field">{{ row.Label }}</label>
          </FloatLabel>
        </div>
      </div>
      <template #footer>
        <div class="dialog-footer">
          <Button label="Отмена" icon="pi pi-times" @click="cancel" text />
          <Button label="Сохранить" icon="pi pi-check" @click="save" />
        </div>
      </template>
    </Dialog>
  </div>
</template>

<style scoped>
.dialog-footer {
  display: flex;
  justify-content: flex-end;
  gap: 0.5rem;
}
</style>

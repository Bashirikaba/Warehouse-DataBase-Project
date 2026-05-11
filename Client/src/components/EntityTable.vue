<script setup lang="ts">
import Api from '@/apiProvider/api'
import type { IEntity, ITableConfigItem } from '@/types/interfaces'
import type { Service } from '@/types/types'
import type { DataTableRowEditInitEvent } from 'primevue'
import { ref, type Ref } from 'vue'

interface IEntityTableProps {
  config: ITableConfigItem[]
  apiService: Service
}

const entityList = defineModel<IEntity[]>({ required: true })
const props = defineProps<IEntityTableProps>()
const editingRows: Ref<IEntity[]> = ref([])
Api.setService(props.apiService)
</script>
<template>
  <DataTable
    :editing-rows="editingRows"
    :value="entityList"
    striped-rows
    size="small"
    show-gridlines
    data-key="Id"
    edit-mode="row"
    @row-edit-init="
      (e: DataTableRowEditInitEvent) => {
        editingRows = [e.data]
      }
    "
  >
    <Column
      v-for="row in config"
      :key="row.Field"
      :field="row.Field"
      :header="row.Label"
      :data-type="row.Type"
      :hidden="row.Hidden"
    >
      <template v-if="row.Type == 'date'" #body="{ data }">
        {{ new Date(data[row.Field]).toLocaleDateString() }}
      </template>
    </Column>
    <Column row-editor></Column>
    <Column>
      <template #body="{ data }">
        <Button
          size="small"
          icon="pi pi-trash"
          variant="outlined"
          rounded
          severity="danger"
          @click="
            async () => {
              await Api.deleteEntity(data.Id)
            }
          "
        /> </template
    ></Column>
  </DataTable>
</template>

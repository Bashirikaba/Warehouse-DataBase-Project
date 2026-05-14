<script setup lang="ts">
import Api from '@/apiProvider/api'
import useFilterHelper from '@/hooks/useFilterHelper'
import type { IEntity, ISearchData, ITableConfigItem } from '@/types/interfaces'
import { onMounted, reactive, ref, type Reactive, type Ref } from 'vue'
import EnumFilterField from './EnumFilterField.vue'
import EntityFormDialog from './EntityFormDialog.vue'
import type { Service } from '@/types/types'
interface IEntityTableProps {
  config: ITableConfigItem[]
  apiService: Service
}

const { initFilter, getStringParam, getNumberParam, getDateParam, getValidatedSearchData } =
  useFilterHelper()
const props = defineProps<IEntityTableProps>()
const entities: Reactive<IEntity[]> = reactive([])
const isEntitiesLoading: Ref<boolean> = ref(true)
const selectedEntity: Ref<IEntity | undefined> = ref(undefined)
const isDialogVisible: Ref<boolean> = ref(false)
const filter: Reactive<ISearchData> = reactive(initFilter(props.config))

Api.setService(props.apiService)

onMounted(() => {
  getEntities()
})

async function getEntities() {
  isEntitiesLoading.value = true

  const response: IEntity[] = await Api.getEntity<IEntity>(getValidatedSearchData(filter))

  entities.length = 0
  Object.assign(entities, response)
  isEntitiesLoading.value = false
}

async function deleteEntity(id: number) {
  await Api.deleteEntity(id)
  await getEntities()
}

function editEntity(entity: IEntity) {
  selectedEntity.value = entity
  isDialogVisible.value = true
}

function createEntity() {
  selectedEntity.value = undefined
  isDialogVisible.value = true
}
</script>
<template>
  <div @keydown.enter="getEntities()">
    <div class="fields">
      <div class="filter" v-for="row in config.filter((r) => r.Field != 'Id')" :key="row.Field">
        <StringFilterField
          v-if="row.Type == 'string'"
          :model-value="getStringParam(filter, row.Field)"
          :label="row.Label"
        />
        <NumberFilterField
          v-if="row.Type == 'number' && !row.Enum"
          :model-value="getNumberParam(filter, row.Field)"
          :label="row.Label"
        />
        <EnumFilterField
          v-if="row.Type == 'number' && row.Enum"
          :model-value="getNumberParam(filter, row.Field)"
          :label="row.Label"
        />
        <DateFilterField
          v-if="row.Type == 'date'"
          :model-value="getDateParam(filter, row.Field)"
          :label="row.Label"
        />
      </div>
    </div>
    <div style="display: flex; gap: 10px; margin-top: 1rem">
      <Button icon="pi pi-search" label="Поиск" @click="getEntities()"></Button>
      <Button icon="pi pi-plus" text label="Создать запись" @click="createEntity()"></Button>
    </div>
    <Divider></Divider>
    <DataTable
      :value="entities"
      striped-rows
      size="small"
      :sort-order="-1"
      sort-mode="single"
      removable-sort
      show-gridlines
      data-key="Id"
      edit-mode="row"
      :loading="isEntitiesLoading"
      @sort="
        async (e) => {
          filter.SortExpression = `${e.sortField} ${e.sortOrder}`
          await getEntities()
        }
      "
    >
      <template #header>
        <div>Всего записей: {{ entities.length }}</div>
      </template>
      <template #empty>
        <div style="display: flex; flex-grow: 1; justify-content: center; padding: 20px">
          <span style="color: lightcoral">Данные не найдены</span>
        </div>
      </template>
      <Column
        v-for="row in config"
        :key="row.Field"
        sortable
        :field="row.Field"
        :header="row.Label"
        :data-type="row.Type"
        :hidden="row.Hidden"
      >
        <template v-if="row.Type == 'date'" #body="{ data }">
          {{ new Date(data[row.Field]).toLocaleDateString() }}
        </template>
        <template #editor="slotProps">
          <div v-if="row.Type == 'string'">
            <InputText fluid v-model="slotProps.data[row.Field]" />
          </div>
        </template>
      </Column>
      <Column>
        <template #body="{ data }">
          <div class="filter-field">
            <Button
              size="small"
              variant="outlined"
              icon="pi pi-pencil"
              rounded
              severity="severity"
              @click="editEntity(data)"
            />
            <Button
              size="small"
              variant="outlined"
              icon="pi pi-trash"
              rounded
              severity="danger"
              @click="
                async () => {
                  await deleteEntity(data.Id)
                }
              "
            />
          </div> </template
      ></Column>
    </DataTable>
    <EntityFormDialog
      v-model="isDialogVisible"
      :edit-data="selectedEntity"
      :config="config"
      :api-service="props.apiService"
      @update="getEntities()"
    ></EntityFormDialog>
  </div>
</template>

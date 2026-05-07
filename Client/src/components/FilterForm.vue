<script setup lang="ts">
import type { IDateParam, INumberParam, ISearchData } from '@/types/interfaces'
import NumberFilterField from './NumberFilterField.vue'
import DateFilterField from './DateFilterField.vue'

interface IFilterFormProps {
  filter: ISearchData
}

defineProps<IFilterFormProps>()
</script>

<template>
  <div class="filter-form" v-for="(params, fIndex) in filter" :key="fIndex">
    <div class="filter-block" v-for="param in params" :key="param.Field">
      <StringFilterField
        v-if="typeof param.Value == 'string'"
        :model="param"
        :label="param.Field"
        @change="$emit('update:filter', filter)"
      />
      <NumberFilterField
        v-if="typeof param.Value == 'number'"
        :model="param as INumberParam"
        :label="param.Field"
      />
      <DateFilterField
        v-if="typeof param.Value == 'object'"
        :model="param as IDateParam"
        :label="param.Field"
      />
    </div>
  </div>
</template>
